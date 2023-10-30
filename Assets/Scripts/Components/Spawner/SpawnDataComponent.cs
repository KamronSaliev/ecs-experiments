using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components
{
    public struct SpawnDataComponent : IComponentData
    {
        public float SpawnRate;
        public Entity Prefab;
    }
}