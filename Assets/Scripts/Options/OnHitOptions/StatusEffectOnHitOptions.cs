namespace MRD
{
    public class WanOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        private int level;
        public WanOnHitOption(int level)
        {
            this.level = level;
        }

        public override void OnHit(EnemyController enemy)
        {
            enemy.GainStatusEffect(EnemyStatusEffectType.WanLoot, level);
        }
    }
    public class PinOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        private int level;
        public PinOnHitOption(int level)
        {
            this.level = level;
        }

        public override void OnHit(EnemyController enemy)
        {
            enemy.GainStatusEffect(EnemyStatusEffectType.PinSlow, level);
        }
    }
}
