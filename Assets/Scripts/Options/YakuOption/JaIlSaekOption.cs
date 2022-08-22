using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class JaIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(JaIlSaekStatOption);

        public override Stat AdditionalStat => new
    (
            damagePercent: 1.0f,
            attackSpeed: 1.5f
    );
    }

    public class JaIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(JaIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            foreach(var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;
                info.AddOnHitOption(new BladeOnHitOption(HolderStat,damageMultiplier: 2.0f));
                info.AddOnHitOption(new ExplosiveOnHitOption(HolderStat, (float)(0.5 + HolderStat.TowerInfo.Hais.Count * 0.1),damageMultiplier: 2.0f));
            }
        }
    }
    public class JaIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(JaIlSaekImageOption);
        protected override List<(int index, int order)> completeTowerImages
        {
            get
            {
                var ret = new List<(int index, int order)>() { (44, 7) };
                if (((YakuHolderInfo)HolderStat.TowerInfo).YakuList.All(x => x.Name is "ShuAnKou" or "ShuKantSu" or "JaIlSaek")) ret.Add((43, 1));
                return ret;
            }
        }
    }
}
