using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitLife : IComponentData
    {
        public float Life;
        public float MaxLife;
    }
}