using System.Linq;

namespace MRD
{
    public class ChanTaStatOption : TowerStatOption
    {
        public override string Name => nameof(ChanTaStatOption);

        public override float AdditionalCritChance => additionalCritChance;

        public override float AdditionalCritMultiplier => additionalCritMultiplier;

        private float additionalCritChance;

        private float additionalCritMultiplier;

        protected override void OnAttachOption()
        {
            var info = (YakuHolderInfo)HolderStat.TowerInfo;

            (additionalCritChance, additionalCritMultiplier) = (info.isMenzen, info is CompleteTowerInfo) switch
            {
                (false, false) => (0.2f, 0f),
                (false, true) => (0.3f, 0f),
                (true, false) => (0.2f, 0.3f),
                (true, true) => (0.3f, 0.4f)
            };
        }
    }
}
