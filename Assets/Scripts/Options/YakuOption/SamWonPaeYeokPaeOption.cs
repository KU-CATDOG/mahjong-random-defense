using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeStatOption : TowerStatOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeStatOption);

        public override float AdditionalAttack => 20;
    }
    public class SamWonPaeYeokPaeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }

    public class SamWonPaeYeokPaeImageOption : TowerImageOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get
            {
                var info = (YakuHolderInfo)HolderStat.TowerInfo;
                return info.MentsuInfos
                    .Where(x => x is KoutsuInfo or KantsuInfo && x.Hais[0].Spec.HaiType == HaiType.Sangen)
                    .Select(x => (x.Hais[0].Spec.Number + 15, 12 + x.Hais[0].Spec.Number))
                    .Append((14, 10))
                    .ToList();
            }
        }
    }
}
