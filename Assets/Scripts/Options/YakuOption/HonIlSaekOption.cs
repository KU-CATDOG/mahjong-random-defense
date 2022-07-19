using System.Collections.Generic;
using System.Linq;
namespace MRD
{
    public class HonIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(HonIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 10.0f;
        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;


    }
    public class HonIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(HonIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: UpdateShupaiLevel 테스트 필요
            // 이 타워의 모든 공격에 2단계 효과 적용(타워 공격 = 1종류)
            
            var haiType = ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos 
                    .Where(x => x is ShuntsuInfo) 
                    .Cast<ShuntsuInfo>().GroupBy(x => x.HaiType) 
                    .Where(g => g.Count() > 2) 
                    .First().Key;
            
            foreach(AttackInfo info in infos)
            {
                info.UpdateShupaiLevel(haiType, 2);
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
                        HaiType.Pin => (7, -1),
                        HaiType.Sou => (8, -1),
                        _ => (0, i + 1)
                    };
                }
                return new() { (image, 1) };
            }

        }
    }
}
