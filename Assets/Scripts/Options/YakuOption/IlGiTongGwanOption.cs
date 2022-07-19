using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class IlGiTongGwanStatOption : TowerStatOption
    {
        public override string Name => nameof(IlGiTongGwanStatOption);

    }
    public class IlGiTongGwanOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(IlGiTongGwanOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
    public class IlGiTongGwanImageOption : TowerImageOption
    {
        public override string Name => nameof(IlGiTongGwanImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get => ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos
                    .Where(x => x is ShuntsuInfo)
                    .Cast<ShuntsuInfo>().GroupBy(x => x.HaiType)
                    .Where(g => g.Count() > 2)
                    .First().Key switch
            {
                HaiType.Wan => new() { (9, 2) },
                HaiType.Pin => new() { (10, 2) },
                HaiType.Sou => new() { (11, 2) },
                _ => new() { },
            };
        }
    }
}
