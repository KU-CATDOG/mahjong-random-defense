using System.Collections.Generic;

namespace MRD
{
    public class HonIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(HonIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 10.0f;
        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;


    }
    public class HonIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(HonIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
