using ECSExperiments.Components.Unit;
using ECSExperiments.Input;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECSExperiments.Selection
{
    [RequireMatchingQueriesForUpdate]
    public partial class SelectionSystem : SystemBase
    {
        private NativeList<Entity> _selectedEntities;
        private SelectionView _selectionView;
        private InputHandler _inputHandler;
        private Camera _camera;

        public struct Singleton : IComponentData
        {
            public NativeList<Entity> SelectedEntities;
        }

        protected override void OnCreate()
        {
            // TODO: refactor
            _inputHandler = Object.FindObjectOfType<InputHandler>(true);
            _selectionView = Object.FindObjectOfType<SelectionView>(true);

            _selectedEntities = new NativeList<Entity>(Allocator.Persistent);

            _camera = Camera.main;

            World.EntityManager.CreateSingleton(new Singleton
            {
                SelectedEntities = _selectedEntities
            });

            _inputHandler.SelectionFinished += OnSelectionFinished;
        }

        protected override void OnDestroy()
        {
            _selectedEntities.Dispose();

            _inputHandler.SelectionFinished -= OnSelectionFinished;
        }

        protected override void OnUpdate()
        {
            if (_inputHandler == null || _selectionView == null)
            {
                return;
            }

            if (_inputHandler.IsSelectionActive)
            {
                _selectionView.Show(_inputHandler.SelectionRect);
            }
        }

        private void OnSelectionFinished()
        {
            var selectionSingleton = SystemAPI.GetSingletonRW<Singleton>();
            var selectedEntities = selectionSingleton.ValueRW.SelectedEntities;
            selectedEntities.Clear();

            foreach (var (_, localTransform, entity) in SystemAPI.Query<Unit, LocalTransform>()
                         .WithEntityAccess())
            {
                var position = _camera.WorldToScreenPoint(localTransform.Position);

                if (_inputHandler.SelectionRect.Contains(position))
                {
                    selectedEntities.Add(entity);
                }
            }

            _selectionView.Hide();
        }
    }
}