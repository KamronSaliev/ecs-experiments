using ECSExperiments.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ECSExperiments.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial struct EnemyAnimationSystem : ISystem
    {
        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private static readonly float MaxSpeedF = 1.0f;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecsSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecs = ecsSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (gameObjectReference, entity) in SystemAPI.Query<GameObjectReference>().WithEntityAccess())
            {
                var gameObject = Object.Instantiate(gameObjectReference.Value);

                ecs.RemoveComponent<GameObjectReference>(entity);

                ecs.AddComponent(entity, new TransformReference
                {
                    Value = gameObject.transform
                });

                ecs.AddComponent(entity, new AnimatorReference
                {
                    Value = gameObject.GetComponent<Animator>()
                });
            }

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