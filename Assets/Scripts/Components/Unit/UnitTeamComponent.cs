using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitTeamComponent : IComponentData
    {
        public UnitTeamID Value;
    }
}