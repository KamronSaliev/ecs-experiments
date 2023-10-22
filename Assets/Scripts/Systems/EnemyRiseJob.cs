using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using ECSExperiments.Components;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Systems
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemyRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(EnemyRiseAspect enemyRiseAspect, [EntityIndexInQuery] int sortKey)
        {
            enemyRiseAspect.Rise(DeltaTime);
            if (!enemyRiseAspect.IsAboveGround)
            {
                return;
            }

            enemyRiseAspect.SetAtGround();
            ECB.RemoveComponent<EnemyRiseRate>(sortKey, enemyRiseAspect.Entity);
        }
    }
}