using System.Collections.Generic;

namespace MRD
{
    public class ChiToiStatOption : TowerStatOption
    {
        public override string Name => nameof(ChiToiStatOption);

        public override float AdditionalAttackSpeedMultiplier => 5.0f;
    }
    public class ChiToiOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ChiToiOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
