using ECSExperiments.Components;
using Unity.Entities;

namespace ECSExperiments.Authoring
{
    public class EnemyBaker : Baker<EnemyMono>
    {
        public override void Bake(EnemyMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new EnemyRiseRate
            {
                Value = authoring.RiseRate
            });

            AddComponent(entity, new EnemyWalkProperties
            {
                Speed = authoring.Speed
            });

            AddComponent(entity, new EnemyDamageProperties
            {
                DamagePerSecond = authoring.DamagePerSecond
            });

            AddComponent<TagNewEnemy>(entity);
        }
    }
}