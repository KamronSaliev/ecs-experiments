using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class PlayerMono : MonoBehaviour
    {
        public float Health => _health;

        [SerializeField] private float _health;
    }
}