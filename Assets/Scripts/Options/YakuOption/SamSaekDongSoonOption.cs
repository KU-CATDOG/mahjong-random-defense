using System.Collections.Generic;

namespace MRD
{
    public class SamSaekDongSoonStatOption : TowerStatOption
    {
        public override string Name => nameof(SamSaekDongSoonStatOption);

        public override float AdditionalAttackSpeedMultiplier => additionalAttackSpeedMultiplier;
        public override float AdditionalAttack => additionalAttack;

        private float additionalAttackSpeedMultiplier;
        private float additionalAttack;

        protected override void OnAttachOption()
        {
            var info = (YakuHolderInfo)HolderStat.TowerInfo;
            var isComplete = info is CompleteTowerInfo;

            (additionalAttack, additionalAttackSpeedMultiplier) = (isComplete, info.isMenzen) switch
            {
                (false, false) => (0f, 1.2f),
                (false, true) => (15f, 1.2f),
                (true, false) => (0f, 1.3f),
                (true, true) => (50f, 1.3f),
            };
        }
    }
    public class SamSaekDongSoonProcessOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamSaekDongSoonProcessOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
