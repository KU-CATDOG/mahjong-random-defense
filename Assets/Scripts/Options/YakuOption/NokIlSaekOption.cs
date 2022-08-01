using System.Collections.Generic;

namespace MRD
{
    public class NokIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(NokIlSaekStatOption);
    }

    public class NokIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(NokIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
