using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components.Common
{
    public struct RandomComponent : IComponentData
    {
        public Random Value;
    }
}