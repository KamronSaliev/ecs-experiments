using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct EnemySpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(GraveyardAspect graveyardAspect)
        {
            graveyardAspect.EnemySpawnTimer -= DeltaTime;

            if (!graveyardAspect.IsTimeToSpawnEnemy() ||
                !graveyardAspect.EnemySpawnPointsInitialized())
            {
                return;
            }

            graveyardAspect.EnemySpawnTimer = graveyardAspect.EnemySpawnRate;
            var newEnemy = ECB.Instantiate(graveyardAspect.EnemyPrefab);

            var newEnemyTransform = graveyardAspect.GetRandomEnemySpawnPointTransform();
            ECB.SetComponent(newEnemy, newEnemyTransform);
        }
    }
}