using System.Linq;
namespace MRD
{
    public class SouReinforcementRelic : Relic
    {
        public override string Name => "SouReinforcement";
        public override int MaxAmount => 10;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(damageConstant: towerStat.TowerInfo.Hais.Count(x => x.Spec.HaiType == HaiType.Sou) * 2);
    }

    public class PinReinforcementRelic : Relic
    {
        public override string Name => "PinReinforcement";
        public override int MaxAmount => 10;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(damageConstant: towerStat.TowerInfo.Hais.Count(x => x.Spec.HaiType == HaiType.Pin) * 2);
    }
    public class WanReinforcementRelic : Relic
    {
        public override string Name => "WanReinforcement";
        public override int MaxAmount => 10;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(damageConstant: towerStat.TowerInfo.Hais.Count(x => x.Spec.HaiType == HaiType.Wan) * 2);
    }
}