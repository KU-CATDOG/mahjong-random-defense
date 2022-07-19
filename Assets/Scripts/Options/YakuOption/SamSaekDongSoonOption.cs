using System.Collections.Generic;
using System;

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
            // FIXME: WIP
            // 무작위 수패 2단계 효과.
            foreach(AttackInfo info in infos)
            {
                if(info is not BulletInfo bulletInfo) continue;
                
                Random rand = new Random();
                int type = rand.Next(2);

                switch(type)
                {
                    case 0:
                        bulletInfo.AddOnHitOption(new PinOnHitOption(2));
                        break;
                    case 1:
                        bulletInfo.AddOnHitOption(new WanOnHitOption(2));
                        break;
                    case 2:
                        bulletInfo.PenetrateLevel = 2;
                        break;
                }
            }
        }
    }
    public class SamSaekDongSoonOption : TowerImageOption
    {
        public override string Name => nameof(SamSaekDongSoonOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (1, 1) };
    }
}
