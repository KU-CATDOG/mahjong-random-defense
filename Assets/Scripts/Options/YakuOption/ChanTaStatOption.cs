using System.Collections.Generic;

namespace MRD
{
    public class ChanTaStatOption : TowerStatOption
    {
        private float additionalCritChance;

        private float additionalCritMultiplier;
        public override string Name => nameof(ChanTaStatOption);

        public override Stat AdditionalStat => new
    (
            critChance: additionalCritChance,
            critDamage: additionalCritMultiplier
    );

        protected override void OnAttachOption()
        {
            var info = (YakuHolderInfo)HolderStat.TowerInfo;

            (additionalCritChance, additionalCritMultiplier) = (info.isMenzen, info is CompleteTowerInfo) switch
            {
                (false, false) => (0.2f, 1.2f),
                (false, true) => (0.3f, 1.6f),
                (true, false) => (0.2f, 1.8f),
                (true, true) => (0.4f, 2.4f),
            };
        }
    }

    public class ChantaImageOption : TowerImageOption
    {
        public override string Name => nameof(ChantaImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (24, 7) };
    }
}
