using ECSExperiments.Components;
using ECSExperiments.Components.Common;
using ECSExperiments.Components.Spawner;
using ECSExperiments.Utilities;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSExperiments.Aspects
{
    public readonly partial struct BattlefieldAspect : IAspect
    {
        private readonly RefRO<BattlefieldDataComponent> _battlefieldData;
        
        private readonly RefRW<RandomComponent> _random;
        
        private readonly RefRO<SpawnDataComponent> _unitSpawnData;
        private readonly RefRW<SpawnPositionsComponent> _unitSpawnPositions;
        private readonly RefRW<TimerComponent> _unitSpawnTimer;
        
        private const int PlayerAreaRadiusSq = 100; // TODO: Serialize in player mono
        private const float DefaultScale = 1.0f;

        public int PositionCount => _battlefieldData.ValueRO.PositionCount;
        public float UnitSpawnRate => _unitSpawnData.ValueRO.SpawnRate;
        public Entity UnitPrefab => _unitSpawnData.ValueRO.Prefab;
        public Entity DemoPrefab => _battlefieldData.ValueRO.DemoPrefab; // TODO: remove later

        public LocalTransform GetRandomBattlefieldPositionTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomBattlefieldPosition(),
                Rotation = quaternion.identity,
                Scale = DefaultScale
            };
        }

        private float3 GetRandomBattlefieldPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _random.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(float3.zero, randomPosition) <= PlayerAreaRadiusSq);

            return randomPosition;
        }

        private float3 MinCorner => float3.zero - HalfDimensions;
        private float3 MaxCorner => float3.zero + HalfDimensions;

        private float3 HalfDimensions => new
        (
            _battlefieldData.ValueRO.Dimensions.x * 0.5f,
            0.0f,
            _battlefieldData.ValueRO.Dimensions.y * 0.5f
        );

        public bool UnitSpawnPositionsInitialized()
        {
            return _unitSpawnPositions.ValueRO.Value.IsCreated && UnitSpawnPositionsCount > 0;
        }

        private int UnitSpawnPositionsCount => _unitSpawnPositions.ValueRO.Value.Value.Value.Length;

        public LocalTransform GetRandomUnitSpawnPositionTransform()
        {
            var position = GetRandomUnitSpawnPosition();

            return new LocalTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(position.GetDirectionTowardsTarget(float3.zero)),
                Scale = DefaultScale
            };
        }

        private float3 GetRandomUnitSpawnPosition()
        {
            var randomIndex = _random.ValueRW.Value.NextInt(0, UnitSpawnPositionsCount);
            return GetUnitSpawnPositionByIndex(randomIndex);
        }

        private float3 GetUnitSpawnPositionByIndex(int index)
        {
            return _unitSpawnPositions.ValueRO.Value.Value.Value[index];
        }

        public bool IsTimeToSpawn()
        {
            return UnitSpawnTimer <= 0.0f;
        }

        public float UnitSpawnTimer
        {
            get => _unitSpawnTimer.ValueRO.Value;
            set => _unitSpawnTimer.ValueRW.Value = value;
        }
    }
}