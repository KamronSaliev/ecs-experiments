using Unity.Entities;
using Unity.Mathematics;

namespace ECSExperiments.Components.Spawner
{
    public struct SpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}