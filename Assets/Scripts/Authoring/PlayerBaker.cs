using ECSExperiments.Components;
using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagPlayer>(entity);

            AddComponent(entity, new PlayerHealth
            {
                Value = authoring.Health,
                Max = authoring.Health
            });

            AddBuffer<PlayerDamageBuffer>(entity);
            
            Debug.Log($"{typeof(PlayerBaker)}");
        }
    }
}