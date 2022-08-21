using System.Collections.Generic;

namespace MRD
{
    public class HonNoDuStatOption : TowerStatOption
    {
        public override string Name => nameof(HonNoDuStatOption);
        public override Stat AdditionalStat => new Stat
            (
                critChance: HolderStat.TowerInfo is CompleteTowerInfo ? 0.75f : 0.6f,
                critDamage: HolderStat.TowerInfo is CompleteTowerInfo ? 3.2f : 2.4f
            );
    }

    public class HonNoDuImageOption : TowerImageOption
    {
        public override string Name => nameof(HonNoDuImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (26, 7) };
    }
}
