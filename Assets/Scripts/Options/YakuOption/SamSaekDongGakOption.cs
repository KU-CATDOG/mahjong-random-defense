using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class SamSaekDongGakStatOption : TowerStatOption
    {
        public override string Name => nameof(SamSaekDongGakStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: HolderStat.TowerInfo is CompleteTowerInfo ? 120f : 60f
    );
    }

    public class SamSaekDongGakOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamSaekDongGakOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // 모든 수패 효과 2단계
            int level = HolderStat.TowerInfo is CompleteTowerInfo ? 2 : 1;
            foreach (var info in infos)
            {
                //if(info is not BulletInfo bulletInfo) continue;
                info.UpdateShupaiLevel(HaiType.Sou, level);
                info.UpdateShupaiLevel(HaiType.Pin, level);
                info.UpdateShupaiLevel(HaiType.Wan, level);
            }
        }
    }

    public class SamSaekDongGakImageOption : TowerImageOption
    {
        public override string Name => nameof(SamSaekDongGakImageOption);

        protected override List<(int index, int order)> tripleTowerImages => ((YakuHolderInfo)HolderStat.TowerInfo).YakuList.Any(x => x.Name.Equals("ToiToi")) ? new() { (2, 3), (29, 4) } : new() { (2, 3) };
    }
}
