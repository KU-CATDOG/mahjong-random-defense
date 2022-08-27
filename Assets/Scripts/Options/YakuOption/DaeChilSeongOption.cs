using System;
using System.Collections.Generic;

namespace MRD
{
    public class DaeChilSeongStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeChilSeongStatOption);

        public override Stat AdditionalStat => new Stat
            (
                damageMultiplier: 2f,
                attackSpeed: 7.0f
            );

        public override TargetTo TargetTo => TargetTo.Spree;
    }

    public class DaeChilSeongOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeChilSeongOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // 탄환이 무작위로 폭발하거나 칼질 공격을 함
            foreach (var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;
                int type = UnityEngine.Random.Range(0, 2);
                info.AddOnHitOption(type == 0
                    ? new ExplosiveOnHitOption(HolderStat, 2f)
                    : new BladeOnHitOption(HolderStat)); 
            }
        }
    }
    public class DaeChilSeongImageOption : TowerImageOption
    {
        public override string Name => nameof(DaeChilSeongImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (38, 1) };
    }
}
