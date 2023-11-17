using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct Unit : IComponentData
    {
        public UnitTeamID TeamID;
    }
}