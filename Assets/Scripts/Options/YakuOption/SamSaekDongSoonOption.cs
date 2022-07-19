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
            // TODO: UpdateShupaiLevel 테스트 필요
            // 무작위 수패 2단계 효과.
            if(HolderStat.TowerInfo is not CompleteTowerInfo){
                foreach(AttackInfo info in infos)
                {   
                    if(info is not BulletInfo bulletInfo) continue;

                    var randomList = new List<HaiType>() { HaiType.Sou, HaiType.Wan, HaiType.Pin };
                    Random rand = new Random();
                    int i = rand.Next(randomList.Count);

                    bulletInfo.UpdateShupaiLevel(randomList[i],1);
                }
                return;
            }

            foreach(AttackInfo info in infos)
            {
                var randomList = new List<HaiType>() { HaiType.Sou, HaiType.Wan, HaiType.Pin };
                Random rand = new Random();
                int i = rand.Next(randomList.Count);

                info.UpdateShupaiLevel(randomList[i],2);
            }
        }
    }
    public class SamSaekDongSoonOption : TowerImageOption
    {
        public override string Name => nameof(SamSaekDongSoonOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (1, 1) };
    }
}
