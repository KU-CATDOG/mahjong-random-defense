using System.Collections.Generic;

namespace MRD
{
    public class RyangPeKoStatOption : TowerStatOption
    {
        public override string Name => nameof(RyangPeKoStatOption);

        public override float AdditionalAttack => 80.0f;
    }

    public class RyangPeKoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(RyangPeKoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
