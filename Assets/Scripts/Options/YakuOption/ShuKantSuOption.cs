using System.Collections.Generic;

namespace MRD
{
    public class ShuKantSuStatOption : TowerStatOption
    {
        public override string Name => nameof(ShuKantSuStatOption);

        public override float AdditionalAttack => 150.0f;
        public override float AdditionalAttackMultiplier => 1.5f;

    }
    public class ShuKantSuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ShuKantSuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
