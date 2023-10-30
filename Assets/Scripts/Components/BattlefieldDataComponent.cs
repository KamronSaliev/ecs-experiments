using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components
{
    public struct BattlefieldDataComponent : IComponentData
    {
        public float2 Dimensions;
        public int PositionCount;
        
        public Entity DemoPrefab; // TODO: remove later
    }
}