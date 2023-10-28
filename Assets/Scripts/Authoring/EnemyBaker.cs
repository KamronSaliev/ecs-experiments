using ECSExperiments.Components;
using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagNewEnemy>(entity);

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

            AddComponentObject(entity, new GameObjectReference
            {
                Value = authoring.Prefab
            });

            Debug.Log($"{typeof(EnemyBaker)}");
        }
    }
}