using ECS.Zombies.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Zombies.Authoring
{
    public class GraveyardBaker : Baker<GraveyardMono>
    {
        public override void Bake(GraveyardMono authoring)
        {
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(graveyardEntity, new GraveyardProperties
            {
                Dimensions = authoring.Dimensions,
                NumberTombstoneToSpawn = authoring.NumberTombstoneToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic)
            });
            
            AddComponent(graveyardEntity, new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}