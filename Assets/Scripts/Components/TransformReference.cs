using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components
{
    public class TransformReference : IComponentData
    {
        public Transform Value;
    }
}