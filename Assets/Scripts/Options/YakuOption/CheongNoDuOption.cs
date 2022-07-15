using System.Collections.Generic;

namespace MRD
{
    public class CheongNoDUStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongNoDUStatOption);

        public override float AdditionalCritChance => HolderStat.TowerInfo is CompleteTowerInfo ? 0.7f : 0.6f;
        public override float AdditionalCritMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 1.0f : 0.6f;
    }
    public class CheongNoDUOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongNoDUOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
