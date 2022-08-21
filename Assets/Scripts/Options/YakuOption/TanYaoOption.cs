using System.Collections.Generic;

namespace MRD
{
    public class TanYaoStatOption : TowerStatOption
    {
        public override string Name => nameof(TanYaoStatOption);

        public override Stat AdditionalStat => new Stat
            (
            damageConstant: HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 20.0f,
            damageMultiplier: HolderStat.TowerInfo is CompleteTowerInfo ? 1.2f : 1.0f
            );
    }

    public class TanYaoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(TanYaoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }

    public class TanYaoImageOption : TowerImageOption
    {
        public override string Name => nameof(TanYaoImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (23, 1) };
    }
}
