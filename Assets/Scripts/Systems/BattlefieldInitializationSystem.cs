using ECSExperiments.Aspects;
using ECSExperiments.Components;
using ECSExperiments.Components.Spawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct BattlefieldInitializationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BattlefieldDataComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var entity = SystemAPI.GetSingletonEntity<BattlefieldDataComponent>();
            var aspect = SystemAPI.GetAspect<BattlefieldAspect>(entity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var unitSpawnPoints = ref builder.ConstructRoot<SpawnPositionsBlob>();
                var arrayBuilder = builder.Allocate(ref unitSpawnPoints.Value, aspect.PositionCount);

                for (var i = 0; i < aspect.PositionCount; i++)
                {
                    var randomPositionTransform = aspect.GetRandomBattlefieldPositionTransform();
                    
                    // TODO: remove later
                    var demoObject = ecb.Instantiate(aspect.DemoPrefab);
                    ecb.SetComponent(demoObject, randomPositionTransform);

                    var newUnitSpawnPosition = randomPositionTransform.Position;
                    arrayBuilder[i] = newUnitSpawnPosition;
                }

                var blobAsset = builder.CreateBlobAssetReference<SpawnPositionsBlob>(Allocator.Persistent);
                ecb.SetComponent(entity, new SpawnPositionsComponent { Value = blobAsset });
            }

            ecb.Playback(state.EntityManager);
        }
    }
}