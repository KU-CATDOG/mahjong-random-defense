using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class JunJJangStatOption : TowerStatOption
    {
        public override string Name => nameof(JunJJangStatOption);
        public override Stat AdditionalStat => new
    (
            critChance: HolderStat.TowerInfo is CompleteTowerInfo ? 0.6f : 0.3f,
            critDamage: HolderStat.TowerInfo is CompleteTowerInfo ? 3.6f : 1.8f,
            damagePercent: ((YakuHolderInfo)HolderStat.TowerInfo).isMenzen ? 0f : -.2f
    );
    }

    public class JunJJangImageOption : TowerImageOption
    {
        public override string Name => nameof(JunJJangImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (25, 7) };
    }
}
