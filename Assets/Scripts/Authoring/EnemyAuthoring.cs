using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float RiseRate => _riseRate;

        public float Speed => _speed;

        public float DamagePerSecond => _damagePerSecond;

        public GameObject Prefab => _prefab;

        [SerializeField] private float _riseRate;
        [SerializeField] private float _speed;
        [SerializeField] private float _damagePerSecond;
        [SerializeField] private GameObject _prefab;
    }
}