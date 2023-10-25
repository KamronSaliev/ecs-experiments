using ECSExperiments.Components;
using Unity.Entities;

namespace ECSExperiments.Authoring
{
    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagPlayer>(entity);

            AddComponent(entity, new PlayerHealth
            {
                Value = authoring.Health,
                Max = authoring.Health
            });

            AddBuffer<PlayerDamageBuffer>(entity);
        }
    }
}