namespace MRD
{
    public class WanOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        private int level;
        public int Level{
            get => level;
            set{ if(level<value) level=value; }
        }
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
        public int Level{
            get => level;
            set{ if(level<value) level=value; }
        }
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
