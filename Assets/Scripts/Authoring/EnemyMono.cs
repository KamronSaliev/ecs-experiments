using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class EnemyMono : MonoBehaviour
    {
        public float RiseRate => _riseRate;

        public float Speed => _speed;

        [SerializeField] private float _riseRate;
        [SerializeField] private float _speed;
    }
}