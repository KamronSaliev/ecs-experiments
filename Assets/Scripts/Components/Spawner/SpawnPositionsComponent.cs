using Unity.Entities;

namespace ECSExperiments.Components.Spawner
{
    public struct SpawnPositionsComponent : IComponentData
    {
        public BlobAssetReference<SpawnPositionsBlob> Value;
    }
}