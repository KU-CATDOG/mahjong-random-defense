using System.Collections.Generic;

namespace MRD
{
    public class JangPungPaeYeokPaeStatOption : TowerStatOption
    {
        public override string Name => nameof(JangPungPaeYeokPaeStatOption);

        public override float AdditionalAttack => 20;
    }
    public class JangPungPaeYeokPaeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(JangPungPaeYeokPaeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
