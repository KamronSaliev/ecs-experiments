using ECSExperiments.Components;
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

            foreach (var (gameObjectReference, entity) in SystemAPI.Query<GameObjectReference>().WithEntityAccess())
            {
                var gameObject = Object.Instantiate(gameObjectReference.Value);

                ecb.RemoveComponent<GameObjectReference>(entity);
                ecb.RemoveComponent<TagNewEnemy>(entity);

                ecb.SetComponentEnabled<EnemyWalkProperties>(entity, false);
                ecb.SetComponentEnabled<EnemyDamageProperties>(entity, false);

                ecb.AddComponent(entity, new TransformReference
                {
                    Value = gameObject.transform
                });

                ecb.AddComponent(entity, new AnimatorReference
                {
                    Value = gameObject.GetComponent<Animator>()
                });
            }

            ecb.Playback(state.EntityManager);
        }
    }
}