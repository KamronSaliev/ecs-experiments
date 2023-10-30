using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class UnitAuthoring : MonoBehaviour
    {
        public GameObject Prefab => _prefab;

        [SerializeField] private GameObject _prefab;
    }
}