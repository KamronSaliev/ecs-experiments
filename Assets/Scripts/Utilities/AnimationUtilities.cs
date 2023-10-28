using ECSExperiments.Components;
using Unity.Mathematics;

namespace ECSExperiments.Utilities
{
    public static class AnimationUtilities
    {
        private static readonly float Tolerance = 0.01f;
        
        public static void TryUpdateFloatParam(this AnimatorReference animatorReference, int parameterHash, float value)
        {
            if (math.abs(animatorReference.Value.GetFloat(parameterHash) - value) < Tolerance)
            {
                animatorReference.Value.SetFloat(parameterHash, value);
            }
        }
    }
}