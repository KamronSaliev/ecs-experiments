using ECSExperiments.Components.Unit;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECSExperiments.Selection
{
    public partial class SelectionSystem : SystemBase
    {
        private NativeList<Entity> _selectedEntities;
        private SelectionView _selectionView;
        private SelectionHandler _selectionHandler;
        private Camera _camera;

        public struct Singleton : IComponentData
        {
            public NativeList<Entity> SelectedEntities;
        }

        protected override void OnCreate()
        {
            // TODO: refactor
            _selectionHandler = Object.FindObjectOfType<SelectionHandler>(true);
            _selectionView = Object.FindObjectOfType<SelectionView>(true);

            _selectedEntities = new NativeList<Entity>(Allocator.Persistent);

            _camera = Camera.main;

            World.EntityManager.CreateSingleton(new Singleton
            {
                SelectedEntities = _selectedEntities
            });

            _selectionHandler.SelectionFinished += OnSelectionFinished;
        }

        protected override void OnDestroy()
        {
            _selectedEntities.Dispose();

            _selectionHandler.SelectionFinished -= OnSelectionFinished;
        }

        protected override void OnUpdate()
        {
            if (_selectionHandler == null || _selectionView == null)
            {
                return;
            }

            if (_selectionHandler.IsSelectionActive)
            {
                _selectionView.Show(_selectionHandler.CurrentSelectionRect);
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

                if (_selectionHandler.CurrentSelectionRect.Contains(position))
                {
                    selectedEntities.Add(entity);
                }
            }

            _selectionView.Hide();
        }
    }
}