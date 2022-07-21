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
    }
}
