using ECSExperiments.Jobs;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemyRiseSystem))]
    public partial struct EnemyWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new EnemyWalkJob
            {
                DeltaTime = deltaTime
                
            }.ScheduleParallel();
        }
    }
}