using ECSExperiments.Mono;
using ProjectDawn.Navigation;
using Unity.Entities;
using Unity.Transforms;
using Object = UnityEngine.Object;

namespace ECSExperiments.Input
{
    public partial class SelectionMeshDrawerSystem : SystemBase
    {
        private SelectionMeshDrawer _selectionMeshDrawer;

        protected override void OnCreate()
        {
            // TODO: refactor
            _selectionMeshDrawer = Object.FindObjectOfType<SelectionMeshDrawer>(true);
        }

        protected override void OnUpdate()
        {
            if (_selectionMeshDrawer == null)
            {
                return;
            }

            var selection = SystemAPI.GetSingleton<SelectionSystem.Singleton>();

            if (selection.SelectedEntities.IsEmpty)
            {
                return;
            }

            var shapeLookup = GetComponentLookup<AgentShape>(true);
            var transformLookup = GetComponentLookup<LocalTransform>(true);

            Dependency.Complete();

            foreach (var entity in selection.SelectedEntities)
            {
                if (!shapeLookup.TryGetComponent(entity, out var shape) ||
                    !transformLookup.TryGetComponent(entity, out var transform))
                {
                    continue;
                }

                // TODO: serialize for different unit scales later
                _selectionMeshDrawer.Draw(transform.Position, shape.Radius * 3.0f);
            }
        }
    }
}