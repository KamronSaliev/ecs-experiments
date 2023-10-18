using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Zombies.Components
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}