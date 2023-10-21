using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components
{
    public struct EnemySpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}