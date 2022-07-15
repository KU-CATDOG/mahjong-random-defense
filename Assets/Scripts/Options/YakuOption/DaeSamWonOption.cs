using System.Collections.Generic;

namespace MRD
{
    public class DaeSamWonStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeSamWonStatOption);

        public override float AdditionalAttackMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 3.0f : 2.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.3f : 0.5f;

    }
    public class DaeSamWonOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeSamWonOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
