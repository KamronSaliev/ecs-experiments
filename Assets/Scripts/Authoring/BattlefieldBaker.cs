using ECSExperiments.Components;
using ECSExperiments.Components.Common;
using ECSExperiments.Components.Spawner;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace ECSExperiments.Authoring
{
    public class BattlefieldBaker : Baker<BattlefieldAuthoring>
    {
        public override void Bake(BattlefieldAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BattlefieldDataComponent
            {
                Dimensions = authoring.Dimensions,
                PositionCount = authoring.PositionsCount,
                
                DemoPrefab = GetEntity(authoring.DemoPrefab, TransformUsageFlags.Dynamic) // TODO: remove later
            });

            AddComponent(entity, new RandomComponent
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            
            AddComponent(entity, new SpawnDataComponent
            {
                SpawnRate = authoring.UnitSpawnRate,
                Prefab = GetEntity(authoring.UnitPrefab, TransformUsageFlags.Dynamic)
            });

            AddComponent<SpawnPositionsComponent>(entity);

            AddComponent<TimerComponent>(entity);
        }
    }
}