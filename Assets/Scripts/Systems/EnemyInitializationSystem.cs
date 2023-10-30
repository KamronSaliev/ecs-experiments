using ECSExperiments.Components;
using ECSExperiments.Components.Common;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyInitializationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (gameObjectReference, entity) in SystemAPI.Query<GameObjectReferenceComponent>().WithEntityAccess())
            {
                var gameObject = Object.Instantiate(gameObjectReference.Value);

                ecb.RemoveComponent<GameObjectReferenceComponent>(entity);
                ecb.RemoveComponent<TagNewEnemy>(entity);

                ecb.SetComponentEnabled<EnemyWalkProperties>(entity, false);
                ecb.SetComponentEnabled<EnemyDamageProperties>(entity, false);

                ecb.AddComponent(entity, new TransformReferenceComponent
                {
                    Value = gameObject.transform
                });

                ecb.AddComponent(entity, new AnimatorReferenceComponent
                {
                    Value = gameObject.GetComponent<Animator>()
                });
            }

            ecb.Playback(state.EntityManager);
        }
    }
}