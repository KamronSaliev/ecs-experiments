using ECS.Zombies.Aspects;
using ECS.Zombies.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS.Zombies.Systems
{
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

            for (var i = 0; i < graveyardAspect.NumberTombstoneToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(graveyardAspect.TombstonePrefab);
                var newTombstoneTransform = graveyardAspect.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone, newTombstoneTransform);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}