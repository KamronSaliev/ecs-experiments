using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Zombies.Components
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 Dimensions;
        public int NumberTombstoneToSpawn;
        public Entity TombstonePrefab;
    }
}