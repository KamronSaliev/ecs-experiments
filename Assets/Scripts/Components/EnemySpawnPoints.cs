using Unity.Entities;

namespace ECSExperiments.Components
{
    public struct EnemySpawnPoints : IComponentData
    {
        public BlobAssetReference<EnemySpawnPointsBlob> Value;
    }
}