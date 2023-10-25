using ECSExperiments.Components;
using ECSExperiments.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECSExperiments.Systems
{
    [UpdateAfter(typeof(EnemyRiseSystem))]
    public partial struct EnemyWalkSystem : ISystem
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
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);

            new EnemyWalkJob
            {
                DeltaTime = deltaTime,
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                PlayerPosition = playerTransform.Position,
                PlayerAreaRadiusSq = 100  // TODO: Serialize in player mono
            }.ScheduleParallel();
        }
    }
}