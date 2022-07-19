using System.Collections.Generic;

namespace MRD
{
    public class SamSaekDongGakStatOption : TowerStatOption
    {
        public override string Name => nameof(SamSaekDongGakStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.3f;
        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 30.0f : 0.0f;


    }
    public class SamSaekDongGakProcessOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamSaekDongGakProcessOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: Pin, Wan 효과가 이미 존재하는 경우 override 해서 적용
            // 모든 수패 효과 2단계
            foreach(AttackInfo info in infos)
            {
                if(info is not BulletInfo bulletInfo) continue;

                bulletInfo.AddOnHitOption(new PinOnHitOption(2));
                bulletInfo.AddOnHitOption(new WanOnHitOption(2));
                bulletInfo.PenetrateLevel = 2;
            }
        }
    }
    public class SamSaekDongGakImageOption : TowerImageOption
    {
        public override string Name => nameof(SamSaekDongGakImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (2, 1) };
    }
}
