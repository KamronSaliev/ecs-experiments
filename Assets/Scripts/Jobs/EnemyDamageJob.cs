using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemyDamageJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity PlayerEntity;

        [BurstCompile]
        private void Execute(EnemyDamageAspect enemyDamageAspect, [ChunkIndexInQuery] int sortKey)
        {
            enemyDamageAspect.Damage(DeltaTime, ECB, sortKey, PlayerEntity);
        }
    }
}