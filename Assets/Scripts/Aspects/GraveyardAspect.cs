using ECSExperiments.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSExperiments.Aspects
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        private readonly RefRO<LocalTransform> _transform;
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        private readonly RefRW<EnemySpawnPoints> _enemySpawnPoints;
        private readonly RefRW<EnemySpawnTimer> _enemySpawnTimer;

        private const int PlayerSafetyRadiusSq = 100;
        private const float DefaultScale = 1.0f;
        private const float MinRotationAngle = -0.25f;
        private const float MaxRotationAngle = 0.25f;

        private LocalTransform Transform => _transform.ValueRO;

        public int NumberTombstoneToSpawn => _graveyardProperties.ValueRO.NumberTombstoneToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;
        public float EnemySpawnRate => _graveyardProperties.ValueRO.EnemySpawnRate;
        public Entity EnemyPrefab => _graveyardProperties.ValueRO.EnemyPrefab;

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
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(Transform.Position, randomPosition) <= PlayerSafetyRadiusSq);

            return randomPosition;
        }

        private quaternion GetRandomTombstoneRotation()
        {
            return quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(MinRotationAngle, MaxRotationAngle));
        }

        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;

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
                Rotation = quaternion.identity, // TODO: rotate towards target
                Scale = DefaultScale
            };
        }

        private float3 GetRandomEnemySpawnPointPosition()
        {
            var randomIndex = _graveyardRandom.ValueRW.Value.NextInt(0, EnemySpawnPointsCount);
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