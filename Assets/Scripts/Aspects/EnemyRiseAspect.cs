using ECSExperiments.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSExperiments.Aspects
{
    public readonly partial struct EnemyRiseAspect : IAspect
    {
        public readonly Entity Owner;
        
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<EnemyRiseRate> _riseRate;

        public void Rise(float deltaTime)
        {
            _transform.ValueRW.Position += math.up() * _riseRate.ValueRO.Value * deltaTime;
        }
        
        public bool IsAboveGround => _transform.ValueRO.Position.y >= 0f;

        public void SetAtGround()
        {
            _transform.ValueRW.Position.y = 0.0f;
        }
    }
}