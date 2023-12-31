using ECSExperiments.Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace ECSExperiments.Authoring
{
    public class GraveyardBaker : Baker<GraveyardAuthoring>
    {
        public override void Bake(GraveyardAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new GraveyardProperties
            {
                Dimensions = authoring.Dimensions,
                NumberTombstoneToSpawn = authoring.NumberTombstoneToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic)
            });

            AddComponent(entity, new EnemySpawnProperties
            {
                SpawnRate = authoring.EnemySpawnRate,
                SpawnOffset = authoring.EnemySpawnOffset,
                EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic)
            });

            AddComponent(entity, new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });

            AddComponent<EnemySpawnPoints>(entity);

            AddComponent<EnemySpawnTimer>(entity);
        }
    }
}