using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class JailSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(JailSaekStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 1.0f : 0.0f;
        public override float AdditionalCritChance => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.6f;
        public override float AdditionalCritMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.6f;
    }

    public class JailSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(JailSaekOption);

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
    public class JailSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(JailSaekImageOption);
        protected override List<(int index, int order)> completeTowerImages
        {
            get
            {
                var ret = new List<(int index, int order)>() { (44, 7) };
                if (((YakuHolderInfo)HolderStat.TowerInfo).YakuList.All(x => x.Name is "ShuAnKou" or "ShuKantSu" or "JailSaek")) ret.Add((43, 1));
                return ret;
            }
        }
    }
}
