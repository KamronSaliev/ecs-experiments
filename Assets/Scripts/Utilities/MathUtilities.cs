using Unity.Mathematics;

namespace ECSExperiments.Utilities
{
    public static class MathUtilities
    {
        public static float GetDirectionTowardsTarget(this float3 position, float3 target)
        {
            var x = target.x - position.x;
            var y = target.z - position.z;
            return math.atan2(x, y);
        }
    }
}