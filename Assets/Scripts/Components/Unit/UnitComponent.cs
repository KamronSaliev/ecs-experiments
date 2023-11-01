using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitComponent : IComponentData
    {
        public UnitTeamID Value;
    }
}