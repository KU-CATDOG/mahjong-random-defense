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

    public class SoSaHeeImageOption : TowerImageOption
    {
        public override string Name => nameof(SoSaHeeImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (35, 1), (36, 6) };
    }
}
