using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitLifeComponent : IComponentData
    {
        public float Life;
        public float MaxLife;
    }
}