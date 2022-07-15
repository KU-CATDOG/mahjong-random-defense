using System.Collections.Generic;

namespace MRD
{
    public class SanKantSuStatOption : TowerStatOption
    {
        public override string Name => nameof(SanKantSuStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.25f : 0.5f;
    }
    public class SanKantSuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SanKantSuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
