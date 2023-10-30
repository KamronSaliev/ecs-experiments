using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components.Spawner
{
    public struct SpawnPositionsBlob
    {
        public BlobArray<float3> Value;
    }
}