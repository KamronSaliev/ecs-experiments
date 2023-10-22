using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class EnemyMono : MonoBehaviour
    {
        public float RiseRate => _riseRate;

        [SerializeField] private float _riseRate = 0.5f;
    }
}