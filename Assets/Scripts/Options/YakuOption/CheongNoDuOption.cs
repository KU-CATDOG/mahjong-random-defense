using System.Collections.Generic;

namespace MRD
{
    public class CheongNoDuStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongNoDuStatOption);

        public override float AdditionalCritChance => HolderStat.TowerInfo is CompleteTowerInfo ? 0.7f : 0.6f;
        public override float AdditionalCritMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 1.0f : 0.6f;
    }

    public class CheongNoDuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongNoDuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class CheongNoDuImageOption : TowerImageOption
    {
        public override string Name => nameof(CheongNoDuImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (45, 6) };
    }
}
