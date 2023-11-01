using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components.Common
{
    public struct RectReferenceComponent : IComponentData
    {
        public Rect Value;
    }
}