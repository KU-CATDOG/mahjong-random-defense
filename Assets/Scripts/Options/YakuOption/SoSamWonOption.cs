using System.Collections.Generic;

namespace MRD
{
    public class SoSamWonStatOption : TowerStatOption
    {
        public override string Name => nameof(SoSamWonStatOption);

        public override float AdditionalAttackMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 2.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.5f;

    }
    public class SoSamWonOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SoSamWonOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
