using Unity.Mathematics;

namespace ECSExperiments.Utilities
{
    public static class MathUtilities
    {
        public static float GetAngleTowardsTarget(this float3 position, float3 target)
        {
            var x = position.x - target.x;
            var y = position.z - target.z;
            return math.atan2(x, y); // TODO: check
        }
    }
}