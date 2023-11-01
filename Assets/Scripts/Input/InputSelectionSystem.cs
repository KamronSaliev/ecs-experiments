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
        private InputSelectionAuthoring _inputSelectionAuthoring;
        private Camera _camera;

        public struct Singleton : IComponentData
        {
            public NativeList<Entity> SelectedEntities;
        }

        protected override void OnCreate()
        {
            _selectedEntities = new NativeList<Entity>(Allocator.Persistent);

            // TODO: refactor
            _inputSelectionAuthoring = Object.FindObjectOfType<InputSelectionAuthoring>(true);
            _inputSelectionView = Object.FindObjectOfType<InputSelectionView>(true);

            _camera = Camera.main;

            World.EntityManager.CreateSingleton(new Singleton
            {
                SelectedEntities = _selectedEntities
            });

            _inputSelectionAuthoring.SelectionFinished += OnSelectionFinished;
        }

        protected override void OnDestroy()
        {
            _selectedEntities.Dispose();
            
            _inputSelectionAuthoring.SelectionFinished -= OnSelectionFinished;
        }

        protected override void OnUpdate()
        {
            if (_inputSelectionAuthoring.IsSelectionActive)
            {
                _inputSelectionView.Show(_inputSelectionAuthoring.CurrentSelectionRect);
            }
        }

        private void OnSelectionFinished()
        {
            var selectionSingleton = SystemAPI.GetSingletonRW<Singleton>();
            var selectedEntities = selectionSingleton.ValueRW.SelectedEntities;
            selectedEntities.Clear();

            foreach (var (_, localTransform, entity) in SystemAPI.Query<UnitComponent, LocalTransform>()
                         .WithEntityAccess())
            {
                var position = _camera.WorldToScreenPoint(localTransform.Position);
                
                if (_inputSelectionAuthoring.CurrentSelectionRect.Contains(position))
                {
                    selectedEntities.Add(entity);
                }
            }

            Debug.Log($"selectedEntities: {selectedEntities.Length}");
            
            _inputSelectionView.Hide();
        }
    }
}