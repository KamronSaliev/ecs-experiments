using ECSExperiments.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct PlayerGetDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
            {
                playerAspect.GetDamage();
            }
        }
    }
}