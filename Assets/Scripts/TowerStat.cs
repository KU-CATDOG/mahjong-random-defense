namespace MRD
{
    public class TowerStat
    {
        public Tower Holder { get; private set; }

        public TowerStat(Tower t)
        {
            Holder = t;
        }

        public int BaseAttack => Holder.TowerInfo.Hais.Count * 10;

        public float BaseAttackSpeed => 1;

        public float BaseCritChance => 0;

        public float BaseCritMultiplier = 2;

        public int FinalAttack { get; private set; }

        public float FinalAttackSpeed { get; private set; }

        public float FinalCritChance { get; private set; }

        public float FinalCritMultiplier { get; private set; }
    }
}
