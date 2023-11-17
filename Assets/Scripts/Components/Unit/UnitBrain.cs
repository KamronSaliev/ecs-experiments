using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitBrain : IComponentData
    {
        public UnitBrainState State;
    }
}