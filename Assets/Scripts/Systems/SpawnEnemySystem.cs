using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    public partial struct SpawnEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new SpawnEnemyJob
            {
                DeltaTime = deltaTime,
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }
}