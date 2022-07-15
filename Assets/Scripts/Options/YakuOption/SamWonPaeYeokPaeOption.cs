using System.Collections.Generic;

namespace MRD
{
    public class SamWonPaeYeokPaeStatOption : TowerStatOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeStatOption);

        public override float AdditionalAttack => 20;
    }
    public class SamWonPaeYeokPaeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
