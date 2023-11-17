using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Confirmation
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class ConfirmationSystem : SystemBase
    {
        private ConfirmationView _confirmationView;

        public struct Singleton : IComponentData
        {
            public float3 Position;
            public bool Play;
        }

        protected override void OnCreate()
        {
            // TODO: refactor
            _confirmationView = Object.FindObjectOfType<ConfirmationView>(true);

            World.EntityManager.CreateSingleton(new Singleton());
        }

        protected override void OnUpdate()
        {
            if (_confirmationView == null)
            {
                return;
            }

            var confirmation = SystemAPI.GetSingleton<Singleton>();

            Dependency.Complete();

            if (confirmation.Play)
            {
                _confirmationView.Play(confirmation.Position); // TODO: add event to play on button click
                confirmation.Play = false;
            }
        }
    }
}