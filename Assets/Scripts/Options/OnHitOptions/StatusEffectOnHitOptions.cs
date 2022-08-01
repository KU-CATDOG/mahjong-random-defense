namespace MRD
{
    public class WanOnHitOption : AttackOnHitOption
    {
        private int level;

        public WanOnHitOption(int level) => this.level = level;

        public override string Name => nameof(WanOnHitOption);

        public int Level
        {
            get => level;
            set
            {
                if (level < value) level = value;
            }
        }

        public override void OnHit(EnemyController enemy)
        {
            enemy.GainStatusEffect(EnemyStatusEffectType.WanLoot, level);
        }
    }

    public class PinOnHitOption : AttackOnHitOption
    {
        private int level;

        public PinOnHitOption(int level) => this.level = level;

        public override string Name => nameof(WanOnHitOption);

        public int Level
        {
            get => level;
            set
            {
                if (level < value) level = value;
            }
        }

        public override void OnHit(EnemyController enemy)
        {
            enemy.GainStatusEffect(EnemyStatusEffectType.PinSlow, level);
        }
    }
}
