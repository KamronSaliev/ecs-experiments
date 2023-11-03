using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Confirmation
{
    public class ConfirmationHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Play1 = Animator.StringToHash("Play");

        public void Play(float3 position)
        {
            if (!_animator.gameObject.activeSelf)
            {
                _animator.gameObject.SetActive(true);
            }

            transform.position = position;
            _animator.SetTrigger(Play1);
        }
    }
}