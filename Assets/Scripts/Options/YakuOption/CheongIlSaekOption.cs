using System.Collections.Generic;

namespace MRD
{
    public class CheongIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 20.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;

    }
    public class CheongIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            
        }
    }
}
