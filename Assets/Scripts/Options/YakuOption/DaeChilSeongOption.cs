using System.Collections.Generic;

namespace MRD
{
    public class DaeChilSeongStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeChilSeongStatOption);

        public override float AdditionalAttackSpeedMultiplier => 5.0f;
    }
    public class DaeChilSeongOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeChilSeongOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
