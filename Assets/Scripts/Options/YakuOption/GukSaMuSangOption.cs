using System.Collections.Generic;

namespace MRD
{
    public class GukSaMuSangStatOption : TowerStatOption
    {
        public override string Name => nameof(GukSaMuSangStatOption);

    }
    public class GukSaMuSangOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(GukSaMuSangOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
