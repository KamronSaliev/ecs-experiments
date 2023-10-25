using ECSExperiments.Components;
using Unity.Entities;

namespace ECSExperiments.Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        private readonly RefRW<PlayerHealth> _playerHealth;
        private readonly DynamicBuffer<PlayerDamageBuffer> _playerDamageBuffer;

        public void GetDamage()
        {
            foreach (var damageBufferElement in _playerDamageBuffer)
            {
                _playerHealth.ValueRW.Value -= damageBufferElement.Value;
            }
            
            _playerDamageBuffer.Clear();
        }
    }
}