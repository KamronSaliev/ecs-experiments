using ECS.Zombies.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Zombies.Authoring
{
    public class GraveyardBaker : Baker<GraveyardMono>
    {
        public override void Bake(GraveyardMono authoring)
        {
            AddComponent(new GraveyardProperties
            {
                Dimensions = authoring.Dimensions,
                NumberTombstoneToSpawn = authoring.NumberTombstoneToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic)
            });
            
            AddComponent(new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}