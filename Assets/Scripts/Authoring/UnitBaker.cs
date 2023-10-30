using ECSExperiments.Components.Common;
using ECSExperiments.Components.Unit;
using Unity.Entities;

namespace ECSExperiments.Authoring
{
    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagUnitNew>(entity);

            AddComponentObject(entity, new GameObjectReferenceComponent
            {
                Value = authoring.Prefab
            });
        }
    }
}