namespace MRD
{
    public class SideSupportRelic : Relic
    {
        public override string Name => "SideSupport";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat) => new(attackSpeed: towerStat.AttachedTower.Coordinate.Y is 0 or 4?1.1f:1f);
    }
}