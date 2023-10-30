using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components.Common
{
    public class TransformReferenceComponent : IComponentData
    {
        public Transform Value;
    }
}