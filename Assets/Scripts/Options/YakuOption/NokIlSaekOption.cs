using System.Collections.Generic;

namespace MRD
{
    public class NokIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(NokIlSaekStatOption);
        public override Stat AdditionalStat => new Stat(damageConstant: 40f, attackSpeed: .8f);
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
    public class NokIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(NokIlSaekImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (46, 1) };
    }
}
