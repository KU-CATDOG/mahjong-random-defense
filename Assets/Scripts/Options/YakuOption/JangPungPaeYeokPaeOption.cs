using System.Collections.Generic;

namespace MRD
{
    public class JangPungPaeYeokPaeStatOption : TowerStatOption
    {
        public override string Name => nameof(JangPungPaeYeokPaeStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: 20f
    );
    }

    public class JangPungPaeYeokPaeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(JangPungPaeYeokPaeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            foreach (var info in infos)
            {
                if(info is not BulletInfo bulletInfo) continue;
                bulletInfo.AddOnHitOption(new BladeOnHitOption(HolderStat));
            }
        }
    }

    public class JangPungPaeYeokPaeImageOption : TowerImageOption
    {
        public override string Name => nameof(JangPungPaeYeokPaeImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (18, 10) };
    }
}
