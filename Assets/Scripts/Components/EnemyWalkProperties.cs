using Unity.Entities;

namespace ECSExperiments.Components
{
    public struct EnemyWalkProperties : IComponentData, IEnableableComponent
    {
        public float Speed;
    }
}