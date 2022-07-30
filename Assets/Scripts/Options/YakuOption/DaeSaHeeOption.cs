using System.Collections.Generic;

namespace MRD
{
    public class DaeSaHeeStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeSaHeeStatOption);

        public override float AdditionalAttackSpeedMultiplier => 0.2f;
        public override float AdditionalAttackPercent => 0.25f;
        public override AttackBehaviour AttackBehaviour => new BladeAttackBehaviour(true);

    }
    public class DaeSaHeeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeSaHeeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
