using System.Collections.Generic;
using System.Linq;
namespace MRD
{
    public class CheongIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 20.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;

    }
    public class CheongIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: 완성타워
            if(HolderStat.TowerInfo is not CompleteTowerInfo) {
                var haiType = ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos
                    .Where(x => x is ShuntsuInfo)
                    .Cast<ShuntsuInfo>().GroupBy(x => x.HaiType)
                    .Where(g => g.Count() > 2)
                    .First().Key;
                foreach(AttackInfo info in infos)
                {
                    if(info is not BulletInfo bulletInfo) continue;
                    bulletInfo.UpdateShupaiLevel(haiType, 2);
                    bulletInfo.SetImage(haiType,2);
                }
                return;
            }
        }
    }
    public class CheongIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(CheongIlSaekImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get => HolderStat.TowerInfo.Hais[0].Spec.HaiType switch
            {
                HaiType.Wan => new() { (3, 1) },
                HaiType.Pin => new() { (4, 1) },
                HaiType.Sou => new() { (5, 1) },
                _ => new() { },
            };

        }
    }
}
