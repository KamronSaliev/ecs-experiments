using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitFollow : IComponentData
    {
        public Entity Target;
        public float MinDistance;
    }
}