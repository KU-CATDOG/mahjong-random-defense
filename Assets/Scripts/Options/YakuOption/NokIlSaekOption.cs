using System.Collections.Generic;

namespace MRD
{
    public class NokIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(NokIlSaekStatOption);
    }

    public class NokIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(NokIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            foreach (var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;

                bulletInfo.AddOnHitOption(new JangpanOnHitOption(HolderStat, (float)(0.5 + HolderStat.TowerInfo.Hais.Count * 0.1)));
            }
        }
    }
}
