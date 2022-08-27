using System.Collections.Generic;

namespace MRD
{
    public class ChanTaStatOption : TowerStatOption
    {
        public override string Name => nameof(ChanTaStatOption);

        public override Stat AdditionalStat => new
    (
            critChance: HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.2f,
            critDamage: HolderStat.TowerInfo is CompleteTowerInfo ? 2.4f : 1.2f,
            damagePercent: ((YakuHolderInfo)HolderStat.TowerInfo).isMenzen ? 0f : -25f
    );
    }

    public class ChantaImageOption : TowerImageOption
    {
        public override string Name => nameof(ChantaImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (24, 7) };
    }
}
