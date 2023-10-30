using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components.Common
{
    public class GameObjectReferenceComponent: IComponentData
    {
        public GameObject Value;
    }
}