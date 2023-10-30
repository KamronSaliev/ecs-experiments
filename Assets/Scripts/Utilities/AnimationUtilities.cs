using ECSExperiments.Components.Common;
using Unity.Mathematics;

namespace ECSExperiments.Utilities
{
    public static class AnimationUtilities
    {
        private static readonly float Tolerance = 0.01f;

        public static void TryUpdateFloatParam(this AnimatorReferenceComponent animatorReferenceComponent,
            int parameterHash, float value)
        {
            if (math.abs(animatorReferenceComponent.Value.GetFloat(parameterHash) - value) < Tolerance)
            {
                animatorReferenceComponent.Value.SetFloat(parameterHash, value);
            }
        }
    }
}