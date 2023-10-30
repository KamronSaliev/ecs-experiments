using ECSExperiments.Components.Common;
using ECSExperiments.Components.Tags;
using Unity.Entities;

namespace ECSExperiments.Authoring
{
    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<TagNewUnit>(entity);

            AddComponentObject(entity, new GameObjectReferenceComponent
            {
                Value = authoring.Prefab
            });
        }
    }
}