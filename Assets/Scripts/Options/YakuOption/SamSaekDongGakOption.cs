using System.Collections.Generic;

namespace MRD
{
    public class SamSaekDongGakStatOption : TowerStatOption
    {
        public override string Name => nameof(SamSaekDongGakStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.3f;
        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 30.0f : 0.0f;


    }
    public class SamSaekDongGakProcessOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamSaekDongGakProcessOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
