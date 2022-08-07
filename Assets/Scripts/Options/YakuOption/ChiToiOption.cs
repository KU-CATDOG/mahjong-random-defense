using System.Collections.Generic;

namespace MRD
{
    public class ChiToiStatOption : TowerStatOption
    {
        public override string Name => nameof(ChiToiStatOption);

        public override Stat AdditionalStat => new
    (
            attackSpeed: 5f
    );

        public override TargetTo TargetTo => TargetTo.Spree;
    }

    public class ChiToiOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ChiToiOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }

    public class ChiToiImageOption : TowerImageOption
    {
        public override string Name => nameof(ChiToiImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (32, 2)};
    }
}
