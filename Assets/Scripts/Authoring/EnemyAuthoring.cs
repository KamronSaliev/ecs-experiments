using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float RiseRate => _riseRate;

        public GameObject Prefab => _prefab;

        [SerializeField] private float _riseRate;
        [SerializeField] private GameObject _prefab;
    }
}