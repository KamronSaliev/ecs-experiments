using ProjectDawn.Navigation;
using Unity.Entities;
using Unity.Transforms;
using Object = UnityEngine.Object;

namespace ECSExperiments.Selection
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

            foreach (var entity in selection.SelectedEntities)
            {
                var shape = EntityManager.GetComponentData<AgentShape>(entity);
                var transform = EntityManager.GetComponentData<LocalTransform>(entity);
                
                // TODO: serialize for different unit scales later
                _selectionMeshDrawer.Draw(transform.Position, shape.Radius * 3.0f);
            }
        }
    }
}