using System.Runtime.InteropServices;
using ECSExperiments.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace ECSExperiments.Jobs
{
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct UnitSpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(BattlefieldAspect battlefieldAspect)
        {
            battlefieldAspect.UnitSpawnTimer -= DeltaTime;

            if (!battlefieldAspect.IsTimeToSpawn() ||
                !battlefieldAspect.UnitSpawnPositionsInitialized())
            {
                return;
            }

            battlefieldAspect.UnitSpawnTimer = battlefieldAspect.UnitSpawnRate;
            var newUnit = ECB.Instantiate(battlefieldAspect.UnitPrefab);

            var newUnitTransform = battlefieldAspect.GetRandomUnitSpawnPositionTransform();
            ECB.SetComponent(newUnit, newUnitTransform);
        }
    }
}