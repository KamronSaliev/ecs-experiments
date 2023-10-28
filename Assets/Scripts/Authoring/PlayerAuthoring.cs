using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float Health => _health;

        [SerializeField] private float _health;
    }
}