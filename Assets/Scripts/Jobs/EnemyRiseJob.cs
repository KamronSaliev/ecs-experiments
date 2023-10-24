using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using ECSExperiments.Components;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemyRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(EnemyRiseAspect enemyRiseAspect, [ChunkIndexInQuery] int sortKey)
        {
            enemyRiseAspect.Rise(DeltaTime);

            if (!enemyRiseAspect.IsAboveGround)
            {
                return;
            }

            enemyRiseAspect.SetAtGround();
            ECB.RemoveComponent<EnemyRiseRate>(sortKey, enemyRiseAspect.Owner);
            ECB.SetComponentEnabled<EnemyWalkProperties>(sortKey, enemyRiseAspect.Owner, true);
        }
    }
}