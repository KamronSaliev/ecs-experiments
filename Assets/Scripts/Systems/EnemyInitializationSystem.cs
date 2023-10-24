using ECSExperiments.Aspects;
using ECSExperiments.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyInitializationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var enemy in SystemAPI.Query<EnemyWalkAspect>().WithAll<NewEnemyTag>())
            {
                ecb.RemoveComponent<NewEnemyTag>(enemy.Owner);
                ecb.SetComponentEnabled<EnemyWalkProperties>(enemy.Owner, false);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}