using ECSExperiments.Components;
using ECSExperiments.Components.Common;
using ECSExperiments.Components.Spawner;
using ECSExperiments.Utilities;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSExperiments.Aspects
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        private readonly RefRO<LocalTransform> _transform;
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<RandomComponent> _random;
        private readonly RefRO<SpawnDataComponent> _enemySpawnProperties;
        private readonly RefRW<SpawnPointsComponent> _enemySpawnPoints;
        private readonly RefRW<TimerComponent> _enemySpawnTimer;
        
        private const int PlayerAreaRadiusSq = 100; // TODO: Serialize in player mono
        private const float DefaultScale = 1.0f;
        private const float MinRotationAngle = -0.25f;
        private const float MaxRotationAngle = 0.25f;

        public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberTombstoneToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;
        
        public float EnemySpawnRate => _enemySpawnProperties.ValueRO.SpawnRate;
        public float3 EnemySpawnOffset => _enemySpawnProperties.ValueRO.SpawnOffset;
        public Entity EnemyPrefab => _enemySpawnProperties.ValueRO.EnemyPrefab;

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomTombstonePosition(),
                Rotation = GetRandomTombstoneRotation(),
                Scale = DefaultScale
            };
        }

        private float3 GetRandomTombstonePosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _random.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_transform.ValueRO.Position, randomPosition) <= PlayerAreaRadiusSq);

            return randomPosition;
        }

        private quaternion GetRandomTombstoneRotation()
        {
            return quaternion.RotateY(_random.ValueRW.Value.NextFloat(MinRotationAngle, MaxRotationAngle));
        }

        private float3 MinCorner => _transform.ValueRO.Position - HalfDimensions;
        private float3 MaxCorner => _transform.ValueRO.Position + HalfDimensions;

        private float3 HalfDimensions => new
        (
            _graveyardProperties.ValueRO.Dimensions.x * 0.5f,
            0.0f,
            _graveyardProperties.ValueRO.Dimensions.y * 0.5f
        );

        public bool EnemySpawnPointsInitialized()
        {
            return _enemySpawnPoints.ValueRO.Value.IsCreated && EnemySpawnPointsCount > 0;
        }

        private int EnemySpawnPointsCount => _enemySpawnPoints.ValueRO.Value.Value.Value.Length; // TODO: refactor

        public LocalTransform GetRandomEnemySpawnPointTransform()
        {
            var position = GetRandomEnemySpawnPointPosition();

            return new LocalTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(position.GetDirectionTowardsTarget(_transform.ValueRO.Position)),
                Scale = DefaultScale
            };
        }

        private float3 GetRandomEnemySpawnPointPosition()
        {
            var randomIndex = _random.ValueRW.Value.NextInt(0, EnemySpawnPointsCount);
            return GetEnemySpawnPoint(randomIndex);
        }

        private float3 GetEnemySpawnPoint(int index)
        {
            return _enemySpawnPoints.ValueRO.Value.Value.Value[index];
        }

        public bool IsTimeToSpawnEnemy()
        {
            return EnemySpawnTimer <= 0.0f;
        }

        public float EnemySpawnTimer
        {
            get => _enemySpawnTimer.ValueRO.Value;
            set => _enemySpawnTimer.ValueRW.Value = value;
        }
    }
}