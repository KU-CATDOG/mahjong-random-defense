using System.Collections.Generic;

namespace MRD
{
    public class JailSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(JailSaekStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 1.0f : 0.0f;
        public override float AdditionalCritChance => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.6f;
        public override float AdditionalCritMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.6f;
    }
    public class JailSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(JailSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
