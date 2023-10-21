using ECSExperiments.Aspects;
using ECSExperiments.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyardAspect = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var enemySpawnPoints = ref builder.ConstructRoot<EnemySpawnPointsBlob>();
                var arrayBuilder = builder.Allocate(ref enemySpawnPoints.Value, graveyardAspect.NumberTombstoneToSpawn);

                for (var i = 0; i < graveyardAspect.NumberTombstoneToSpawn; i++)
                {
                    var newTombstone = ecb.Instantiate(graveyardAspect.TombstonePrefab);
                    var newTombstoneTransform = graveyardAspect.GetRandomTombstoneTransform();
                    ecb.SetComponent(newTombstone, newTombstoneTransform);

                    var newEnemySpawnPoint = newTombstoneTransform.Position;
                    arrayBuilder[i] = newEnemySpawnPoint;
                }

                var blobAsset = builder.CreateBlobAssetReference<EnemySpawnPointsBlob>(Allocator.Persistent);
                ecb.SetComponent(graveyardEntity, new EnemySpawnPoints { Value = blobAsset });
            }

            ecb.Playback(state.EntityManager);
        }
    }
}