using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Confirmation
{
    public partial class ConfirmationSystem : SystemBase
    {
        private ConfirmationHandler _confirmationHandler;

        public struct Singleton : IComponentData
        {
            public float3 Position;
        }

        protected override void OnCreate()
        {
            // TODO: refactor
            _confirmationHandler = Object.FindObjectOfType<ConfirmationHandler>(true);

            World.EntityManager.CreateSingleton(new Singleton());
        }

        protected override void OnUpdate()
        {
            if (_confirmationHandler == null)
            {
                return;
            }

            var confirmation = SystemAPI.GetSingleton<Singleton>();
            
            _confirmationHandler.Play(confirmation.Position); // TODO: add event to play on button click
        }
    }
}