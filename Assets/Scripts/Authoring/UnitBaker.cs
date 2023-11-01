using ECSExperiments.Components.Common;
using ECSExperiments.Components.Unit;
using ProjectDawn.Navigation;
using Unity.Entities;

namespace ECSExperiments.Authoring
{
    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagUnitNew>(entity);
            
            AddComponent<DrawGizmos>(entity);

            AddComponent(entity, new UnitComponent
            {
                Value = authoring.UnitTeamID
            });

            AddComponent(entity, new UnitStateComponent
            {
                Value = UnitState.Idle
            });

            AddComponent(entity, new UnitLifeComponent
            {
                Life = authoring.MaxLife,
                MaxLife = authoring.MaxLife
            });

            AddComponentObject(entity, new GameObjectReferenceComponent
            {
                Value = authoring.Prefab
            });
        }
    }
}