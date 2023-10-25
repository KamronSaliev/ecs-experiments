using ECSExperiments.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSExperiments.Aspects
{
    public readonly partial struct EnemyWalkAspect : IAspect
    {
        public readonly Entity Owner;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<EnemyWalkProperties> _walkProperties;

        public void Walk(float deltaTime)
        {
            _transform.ValueRW.Position += _transform.ValueRW.Forward() * _walkProperties.ValueRO.Speed * deltaTime;
        }

        public bool IsNearPlayer(float3 playerPosition, float playerAreaRadiusSq)
        {
            return math.distancesq(playerPosition, _transform.ValueRO.Position) <= playerAreaRadiusSq;
        }
    }
}