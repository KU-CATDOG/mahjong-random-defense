using System.Collections.Generic;

namespace MRD
{
    public class PingHuStatOption : TowerStatOption
    {
        public override string Name => nameof(PingHuStatOption);

        public override Stat AdditionalStat => new Stat
            (
                attackSpeed: HolderStat.TowerInfo is CompleteTowerInfo ? 2f : 1.5f
            );
            
    }

    public class PingHuImageOption : TowerImageOption
    {
        public override string Name => nameof(PingHuImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (12, 1) };
    }
}
