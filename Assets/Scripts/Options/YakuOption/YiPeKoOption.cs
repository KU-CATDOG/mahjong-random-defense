using System.Collections.Generic;

namespace MRD
{
    public class YiPeKoStatOption : TowerStatOption
    {
        public override string Name => nameof(YiPeKoStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 40.0f : 20.0f;
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
