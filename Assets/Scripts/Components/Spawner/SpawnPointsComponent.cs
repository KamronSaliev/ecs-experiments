using Unity.Entities;

namespace ECSExperiments.Components.Spawner
{
    public struct SpawnPointsComponent : IComponentData
    {
        public BlobAssetReference<SpawnPointsBlob> Value;
    }
}