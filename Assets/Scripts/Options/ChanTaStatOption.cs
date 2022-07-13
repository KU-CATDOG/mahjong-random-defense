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
            var info = (YakuHolderInfo)HolderInfo;
            var isMenzen = info.MentsuInfos.All(x => x.IsMenzen);
            var isComplete = info is CompleteTowerInfo;

            switch (isMenzen, isComplete)
            {
                case (false, false):
                    additionalCritChance = 0.2f;
                    break;
                case (false, true):
                    additionalCritChance = 0.3f;
                    break;
                case (true, false):
                    additionalCritChance = 0.2f;
                    additionalCritMultiplier = 0.3f;
                    break;
                case (true, true):
                    additionalCritChance = 0.3f;
                    additionalCritMultiplier = 0.4f;
                    break;
            }
        }
    }
}
