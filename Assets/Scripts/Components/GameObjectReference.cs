using Unity.Entities;
using UnityEngine;

namespace ECSExperiments.Components
{
    public class GameObjectReference: IComponentData
    {
        public GameObject Value;
    }
}