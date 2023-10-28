using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components
{
    public struct EnemySpawnProperties : IComponentData
    {
        public float SpawnRate;
        public float3 SpawnOffset;
        public Entity EnemyPrefab;
    }
}