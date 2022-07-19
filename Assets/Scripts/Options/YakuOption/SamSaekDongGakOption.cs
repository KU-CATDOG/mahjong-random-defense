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
            // TODO: UpdateShupaiLevel 테스트 필요
            // 모든 수패 효과 2단계
            int level = HolderStat.TowerInfo is CompleteTowerInfo ? 2 : 1;
            foreach(AttackInfo info in infos)
            {
                //if(info is not BulletInfo bulletInfo) continue;
                info.UpdateShupaiLevel(HaiType.Sou,level);
                info.UpdateShupaiLevel(HaiType.Wan,level);
                info.UpdateShupaiLevel(HaiType.Pin,level);
            }
        }
    }
    public class SamSaekDongGakImageOption : TowerImageOption
    {
        public override string Name => nameof(SamSaekDongGakImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (2, 1) };
    }
}
