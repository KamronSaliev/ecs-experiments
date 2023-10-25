using ECSExperiments.Components;
using Unity.Entities;

namespace ECSExperiments.Aspects
{
    public readonly partial struct EnemyDamageAspect : IAspect
    {
        private readonly RefRO<EnemyDamageProperties> _damageProperties;

        public void Damage(float deltaTime, EntityCommandBuffer.ParallelWriter ecb, int sortKey, Entity playerEntity)
        {
            var damage = _damageProperties.ValueRO.DamagePerSecond * deltaTime;

            var currentDamage = new PlayerDamageBuffer
            {
                Value = damage
            };

            ecb.AppendToBuffer(sortKey, playerEntity, currentDamage);
        }
    }
}