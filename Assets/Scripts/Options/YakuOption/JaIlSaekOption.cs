using System.Collections.Generic;

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
}
