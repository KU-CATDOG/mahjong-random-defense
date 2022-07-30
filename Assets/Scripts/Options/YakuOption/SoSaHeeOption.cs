using System.Collections.Generic;

namespace MRD
{
    public class SoSaHeeStatOption : TowerStatOption
    {
        public override string Name => nameof(SoSaHeeStatOption);

        public override float AdditionalAttackSpeedMultiplier => 0.2f;
        public override AttackBehaviour AttackBehaviour => new BladeAttackBehaviour(false);
    }
    public class SoSaHeeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SoSaHeeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
