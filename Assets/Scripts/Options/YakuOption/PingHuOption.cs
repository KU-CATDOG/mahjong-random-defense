using System.Collections.Generic;

namespace MRD
{
    public class PingHuStatOption : TowerStatOption
    {
        public override string Name => nameof(PingHuStatOption);

        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 1.6f : 1.5f;
    }

}
