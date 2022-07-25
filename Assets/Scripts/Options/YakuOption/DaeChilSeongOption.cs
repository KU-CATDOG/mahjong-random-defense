using System.Collections.Generic;
using System;
namespace MRD
{
    public class DaeChilSeongStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeChilSeongStatOption);

        public override float AdditionalAttackSpeedMultiplier => 5.0f;

        public override TargetTo TargetTo => TargetTo.Random;
    }
    public class DaeChilSeongOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeChilSeongOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // 탄환이 무작위로 폭발하거나 칼질 공격을 함
            Random rand = new Random();
            foreach(var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;
                int type = rand.Next(0,1);
                info.AddOnHitOption(type==0?new ExplosiveOnHitOption(HolderStat,(float)(0.5 + HolderStat.TowerInfo.Hais.Count * 0.1)): new BladeOnHitOption(HolderStat));
            }
        }
    }
}
