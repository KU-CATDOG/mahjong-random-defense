using System.Collections.Generic;

namespace MRD
{
    public class ShuAnKouStatOption : TowerStatOption
    {
        public override string Name => nameof(ShuAnKouStatOption);

        public override float AdditionalAttack => 100.0f;
        public override float AdditionalAttackSpeedMultiplier => 1.5f;

    }
    public class ShuAnKouOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ShuAnKouOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
