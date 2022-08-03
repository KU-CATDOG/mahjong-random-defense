using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ShuKantSuStatOption : TowerStatOption
    {
        public override string Name => nameof(ShuKantSuStatOption);

        public override float AdditionalAttack => 150.0f;
        public override float AdditionalAttackMultiplier => 1.5f;
    }

    public class ShuKantSuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ShuKantSuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: 도라 랭크업
            // 탄환 삭제. +-40도 내에 50% 느린 추가탄환 8개. 앞으로의 도라가 랭크업 됨(무작위 B랭크 도라, 2개시 무작위 A랭크 도라)
            if (infos[0] is not BulletInfo info) return;
            infos.RemoveAt(0);

            var rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                float angle = (float)(rand.NextDouble() * 80d - 40d); // -40f ~ 40f
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, angle), info.SpeedMultiplier / 2f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            }
        }
    }
    public class ShuKantSuImageOption : TowerImageOption
    {
        public override string Name => nameof(ShuKantSuImageOption);
        protected override List<(int index, int order)> completeTowerImages
        {
            get
            {
                var ret = new List<(int index, int order)>() { (42, 3) };
                if (((YakuHolderInfo)HolderStat.TowerInfo).YakuList.All(x => x.Name is "ShuAnKou" or "ShuKantSu")) ret.Add((41, 1));
                return ret;
            }
        }
    }
}
