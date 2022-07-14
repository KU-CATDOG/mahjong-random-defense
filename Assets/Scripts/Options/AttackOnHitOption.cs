namespace MRD
{
    public abstract class AttackOnHitOption
    {
        public abstract string Name { get; }

        public AttackInfo AttackInfo { get; private set; }

        public void AttachOption(AttackInfo attackInfo)
        {
            AttackInfo = attackInfo;
        }

        public abstract void OnHit(EnemyController enemy);
    }
}
