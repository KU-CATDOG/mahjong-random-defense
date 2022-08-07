using System.Collections.Generic;

namespace MRD
{
    public class RyangPeKoStatOption : TowerStatOption
    {
        public override string Name => nameof(RyangPeKoStatOption);
        public override Stat AdditionalStat => new
    (
            damageConstant: 80f
    );

        public override int MaxRagePoint => 10000;

        public override Stat RageStat => new
            (
                damagePercent: .03f,
                attackSpeed: 1.0002f,
                critChance : .01f,
                critDamage: .05f
            );
    }

    public class RyangPeKoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(RyangPeKoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class RyangPeKoImageOption : TowerImageOption
    {
        public override string Name => nameof(RyangPeKoImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (31, 2) };
    }
}
