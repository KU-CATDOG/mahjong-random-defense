using System.Collections.Generic;

namespace MRD
{
    public class YiPeKoStatOption : TowerStatOption
    {
        public override string Name => nameof(YiPeKoStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: HolderStat.TowerInfo is CompleteTowerInfo ? 40.0f : 20.0f
    );

        public override int MaxRagePoint => HolderStat.TowerInfo is CompleteTowerInfo ? 5000 : 3000;

        public override Stat RageStat => new
            (
                damagePercent: HolderStat.TowerInfo is CompleteTowerInfo ? 0.02f : 0.01f,
                attackSpeed: 1.0001f
            );
    }

    public class YiPeKoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(YiPeKoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }

    public class YiPeKoImageOption : TowerImageOption
    {
        public override string Name => nameof(YiPeKoImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (13, 2) };
    }
}
