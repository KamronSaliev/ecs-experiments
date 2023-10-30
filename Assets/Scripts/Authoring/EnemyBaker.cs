using ECSExperiments.Components;
using ECSExperiments.Components.Common;
using ECSExperiments.Components.Tags;
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

            AddComponentObject(entity, new GameObjectReferenceComponent
            {
                Value = authoring.Prefab
            });
        }
    }
}