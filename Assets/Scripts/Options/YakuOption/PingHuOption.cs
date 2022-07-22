using System.Collections.Generic;

namespace MRD
{
    public class PingHuStatOption : TowerStatOption
    {
        public override string Name => nameof(PingHuStatOption);

        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 1.6f : 1.5f;
    }

    public class PingHuImageOption : TowerImageOption
    {
        public override string Name => nameof(PingHuImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (12, 2) };
    }
}
