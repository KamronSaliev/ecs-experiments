using ECS.Zombies.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Zombies.Aspects
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        private readonly RefRO<LocalTransform> _transform;
        private readonly RefRO<GraveyardProperties> _properties;
        private readonly RefRW<GraveyardRandom> _random;

        private const int BrainSafetyRadiusSq = 100;

        private LocalTransform Transform => _transform.ValueRO;
        
        public int NumberTombstoneToSpawn => _properties.ValueRO.NumberTombstoneToSpawn;
        public Entity TombstonePrefab => _properties.ValueRO.TombstonePrefab;

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1.0f
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _random.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(Transform.Position, randomPosition) <= BrainSafetyRadiusSq);

            return randomPosition;
        }

        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        private float3 HalfDimensions => new
        (
            _properties.ValueRO.Dimensions.x * 0.5f,
            0.0f,
            _properties.ValueRO.Dimensions.y * 0.5f
        );
    }
}