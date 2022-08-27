namespace MRD
{
    public class ThroneRelic : Relic
    {
        public override string Name => "Throne";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.S;
        public override Stat AdditionalStat(TowerStat towerStat)
        {
            if(towerStat.AttachedTower.Coordinate.Equals(4,2)) return new(damageMultiplier: 1.8f,attackSpeed: 1.2f,critChance: 0.2f, critDamage: 1f);
            return new();
        }

    }
}