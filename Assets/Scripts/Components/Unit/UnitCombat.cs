using Unity.Entities;

namespace ECSExperiments.Components.Unit
{
    public struct UnitCombat : IComponentData
    {
        public Entity Target;
        public float Damage;
        public float AggressionRadius;
        public float Range;
        public float Speed;
        public float Cooldown;
        public double CooldownTime;
        public double AttackTime;
        public bool IsReady(double time) => time >= CooldownTime + Cooldown;
        public bool IsFinished(double time) => time >= AttackTime + Speed;
    }
}