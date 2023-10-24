using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemyWalkJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(EnemyWalkAspect enemyWalkAspect)
        {
            enemyWalkAspect.Walk(DeltaTime);
        }
    }
}