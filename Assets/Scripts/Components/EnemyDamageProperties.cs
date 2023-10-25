using Unity.Entities;

namespace ECSExperiments.Components
{
    public struct EnemyDamageProperties : IComponentData, IEnableableComponent
    {
        public float DamagePerSecond;
    }
}