using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class HonIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(HonIlSaekStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: HolderStat.TowerInfo is CompleteTowerInfo ? 40f : 20f,
            damagePercent: ((YakuHolderInfo)HolderStat.TowerInfo).isMenzen ? 0f : -.2f
    );
    }

    public class HonIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(HonIlSaekOption);
        private HaiType? honilType = null;
        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // 이 타워의 모든 공격에 2단계 효과 적용(타워 공격 = 1종류)
            int targetLevel = HolderStat.TowerInfo is CompleteTowerInfo ? 2 : 1;
            if (honilType == null)
                honilType = ((YakuHolderInfo)HolderStat.TowerInfo).Hais.First( x => !x.Spec.IsJi).Spec.HaiType;
                
            foreach (var info in infos)
            {
                info.UpdateShupaiLevel((HaiType)honilType, targetLevel);
                info.SetImage((HaiType)honilType, 2);
            }
        }
    }

    public class HonIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(HonIlSaekImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get
            {
                int i = 0;
                int image = 0;
                var hais = HolderStat.TowerInfo.Hais;
                while (i >= 0)
                {
                    (image, i) = hais[i].Spec.HaiType switch
                    {
                        HaiType.Wan => (6, -1),
                        HaiType.Sou => (7, -1),
                        HaiType.Pin => (8, -1),
                        _ => (0, i + 1)
                    };
                }
                return new() { (image, 3) };
            }
        }
    }
}
