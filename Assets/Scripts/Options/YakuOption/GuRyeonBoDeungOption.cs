using System.Collections.Generic;

namespace MRD
{
    public class GuRyeonBoDeungStatOption : TowerStatOption
    {
        public override string Name => nameof(GuRyeonBoDeungStatOption);

        public override float AdditionalAttackSpeedMultiplier => 0.2f;
        public override float AdditionalCritChance => 0.4f;
        public override float AdditionalCritMultiplier => 0.5f;

    }
    public class GuRyeonBoDeungOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(GuRyeonBoDeungOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
}
