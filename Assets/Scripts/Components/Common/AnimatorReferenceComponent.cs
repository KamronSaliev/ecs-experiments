using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components.Common
{
    public class AnimatorReferenceComponent : IComponentData
    {
        public Animator Value;
    }
}