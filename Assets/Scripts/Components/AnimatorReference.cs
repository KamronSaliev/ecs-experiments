using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components
{
    public class AnimatorReference : IComponentData
    {
        public Animator Value;
    }
}