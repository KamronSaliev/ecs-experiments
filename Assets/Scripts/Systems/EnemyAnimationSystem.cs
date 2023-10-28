using ECSExperiments.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECSExperiments.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial struct EnemyAnimationSystem : ISystem
    {
        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private static readonly float MaxSpeedF = 1.0f;

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, transformReference, animatorReference) in SystemAPI
                         .Query<LocalTransform, TransformReference, AnimatorReference>())
            {
                transformReference.Value.position = transform.Position;
                transformReference.Value.rotation = transform.Rotation;
                animatorReference.Value.SetFloat(SpeedF, MaxSpeedF);
            }
        }
    }
}