using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeStatOption : TowerStatOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: 20f
    );
    }

    public class SamWonPaeYeokPaeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SamWonPaeYeokPaeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            foreach (var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;
                
                info.AddOnHitOption(new GrenadeOnHitOption(HolderStat));
                for(int i=0;i<RoundManager.Inst.RelicManager[typeof(AdditionalExplosionRelic)];i++)
                    if(UnityEngine.Random.Range(0f,1f) < 0.3f)
                        info.AddOnHitOption(new GrenadeOnHitOption(HolderStat));
            }
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
                    .Select(x => (x.Hais[0].Spec.Number + 15, 13 + x.Hais[0].Spec.Number))
                    .Append((14, 10))
                    .ToList();
            }
        }
    }
}
