using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitStateComponent : IComponentData
    {
        public UnitState Value;
    }
}