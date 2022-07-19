using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class JunJJangStatOption : TowerStatOption
    {
        public override string Name => nameof(JunJJangStatOption);

        public override float AdditionalCritChance => additionalCritChance;

        public override float AdditionalCritMultiplier => additionalCritMultiplier;

        private float additionalCritChance;

        private float additionalCritMultiplier;

        protected override void OnAttachOption()
        {
            var info = (YakuHolderInfo)HolderStat.TowerInfo;
            var isMenzen = info.MentsuInfos.All(x => x.IsMenzen);
            var isComplete = info is CompleteTowerInfo;

            switch (isMenzen, isComplete)
            {
                case (false, false):
                    additionalCritChance = 0.4f;
                    additionalCritMultiplier = 0.4f;
                    break;
                case (false, true):
                    additionalCritChance = 0.5f;
                    additionalCritMultiplier = 0.6f;
                    break;
                case (true, false):
                    additionalCritChance = 0.4f;
                    additionalCritMultiplier = 0.8f;
                    break;
                case (true, true):
                    additionalCritChance = 0.6f;
                    additionalCritMultiplier = 1.0f;
                    break;
            }
        }
    }
    public class JunJJangImageOption : TowerImageOption
    {
        public override string Name => nameof(JunJJangImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (25, 1) };
    }
}
