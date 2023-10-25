using Unity.Entities;

namespace ECSExperiments.Components
{
    public struct PlayerHealth : IComponentData
    {
        public float Value;
        public float Max;
    }
}