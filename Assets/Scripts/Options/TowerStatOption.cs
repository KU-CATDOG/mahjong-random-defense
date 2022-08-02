namespace MRD
{
    public abstract class TowerStatOption : TowerOption
    {
        public virtual float AdditionalAttack => 0;

        public virtual float AdditionalAttackPercent => 0;

        public virtual float AdditionalAttackSpeedMultiplier => 1;

        public virtual float AdditionalCritChance => 0;

        public virtual float AdditionalCritMultiplier => 0;

        public virtual float AdditionalAttackMultiplier => 1;

        public virtual AttackBehaviour AttackBehaviour => null;

        public virtual TargetTo TargetTo => TargetTo.Proximity;

        public virtual int MaxRagePoint => 0;

        public virtual float RageAttackPercent => 0f;

        public virtual float RageAttackSpeedMultiplier => 0f;

        public virtual float RageCritChance => 0f;

        public virtual float RageCritMultiplier => 0f;
    }
}
