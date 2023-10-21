using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}