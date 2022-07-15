using System.Collections.Generic;

namespace MRD
{
    public class IlGiTongGwanStatOption : TowerStatOption
    {
        public override string Name => nameof(IlGiTongGwanStatOption);

    }
    public class IlGiTongGwanOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(IlGiTongGwanOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
