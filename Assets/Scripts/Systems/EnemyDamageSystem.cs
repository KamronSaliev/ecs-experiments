using ECSExperiments.Components;
using ECSExperiments.Jobs;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemyWalkSystem))]
    public partial struct EnemyDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TagPlayer>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var playerEntity = SystemAPI.GetSingletonEntity<TagPlayer>();

            new EnemyDamageJob
            {
                DeltaTime = deltaTime,
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                PlayerEntity = playerEntity
            }.ScheduleParallel();
        }
    }
}