using System.Collections.Generic;

namespace MRD
{
    public class HonNoDuStatOption : TowerStatOption
    {
        public override string Name => nameof(HonNoDuStatOption);

        public override float AdditionalCritChance => HolderStat.TowerInfo is CompleteTowerInfo ? 0.75f : 0.6f;
        public override float AdditionalCritMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.80f : 0.6f;

    }
}
