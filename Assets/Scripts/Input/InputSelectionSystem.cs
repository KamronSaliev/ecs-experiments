using ECSExperiments.Components.Unit;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECSExperiments.Input
{
    public partial class InputSelectionSystem : SystemBase
    {
        private NativeList<Entity> _selectedEntities;
        private InputSelectionView _inputSelectionView;
        private InputControlsAuthoring _inputControlsAuthoring;
        private Camera _camera;

        public struct Singleton : IComponentData
        {
            public NativeList<Entity> SelectedEntities;
        }

        protected override void OnCreate()
        {
            _selectedEntities = new NativeList<Entity>(Allocator.Persistent);

            // TODO: refactor
            _inputControlsAuthoring = Object.FindObjectOfType<InputControlsAuthoring>(true);
            _inputSelectionView = Object.FindObjectOfType<InputSelectionView>(true);
            
            _camera = Camera.main;

            World.EntityManager.CreateSingleton(new Singleton
            {
                SelectedEntities = _selectedEntities
            });

            _inputControlsAuthoring.SelectionFinished += OnSelectionFinished;
        }

        protected override void OnDestroy()
        {
            _selectedEntities.Dispose();
            
            _inputControlsAuthoring.SelectionFinished -= OnSelectionFinished;
        }

        protected override void OnUpdate()
        {
            if (_inputControlsAuthoring.IsSelectionActive)
            {
                _inputSelectionView.Show(_inputControlsAuthoring.CurrentSelectionRect);
            }
        }
        
        // TODO: Refactor
        private void OnSelectionFinished()
        {
            var selectionSingleton = SystemAPI.GetSingletonRW<Singleton>();
            var selectedEntities = selectionSingleton.ValueRW.SelectedEntities;
            
            selectedEntities.Clear();

            foreach (var (_, localTransform, entity) in SystemAPI.Query<UnitComponent, LocalTransform>()
                         .WithEntityAccess())
            {
                var position = _camera.WorldToScreenPoint(localTransform.Position);

                if (_inputControlsAuthoring.CurrentSelectionRect.Contains(position))
                {
                    selectedEntities.Add(entity);
                }
            }
            
            Debug.Log($"selectedEntities: {selectedEntities.Length}");

            _inputSelectionView.Hide();
        }
    }
}