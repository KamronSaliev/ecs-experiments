using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using ECSExperiments.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemyWalkJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public float3 PlayerPosition;
        public float PlayerAreaRadiusSq;

        [BurstCompile]
        private void Execute(EnemyWalkAspect enemyWalkAspect, [ChunkIndexInQuery] int sortKey)
        {
            enemyWalkAspect.Walk(DeltaTime);

            if (!enemyWalkAspect.IsNearPlayer(PlayerPosition, PlayerAreaRadiusSq))
            {
                return;
            }

            ECB.SetComponentEnabled<EnemyWalkProperties>(sortKey, enemyWalkAspect.Owner, false);
            ECB.SetComponentEnabled<EnemyDamageProperties>(sortKey, enemyWalkAspect.Owner, true);
        }
    }
}