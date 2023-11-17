using Unity.Mathematics;

namespace ECSExperiments.Utilities
{
    public static class GeometryUtilities
    {
        /// <summary>
        ///     Based on http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html (There is some mistakes
        ///     in proposed solution).
        /// </summary>
        public static bool RaySphereIntersection(float3 origin, float3 direction, float3 center, float radius,
            out float t1, out float t2)
        {
            t1 = 0;
            t2 = 0;

            // solve for tc
            var L = center - origin;
            var tc = math.dot(L, direction);
            if (tc < 0.0)
            {
                return false;
            }

            var d = math.sqrt(math.lengthsq(L) - tc * tc);
            if (d > radius)
            {
                return false;
            }

            // solve for t1c
            var t1c = math.sqrt(radius * radius - d * d);

            // solve for intersection points
            t1 = tc - t1c;
            t2 = tc + t1c;

            return true;
        }
    }
}