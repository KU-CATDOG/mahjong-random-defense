using System;
using System.Collections.Generic;

namespace MRD
{
    public class SanKantSuStatOption : TowerStatOption
    {
        public override string Name => nameof(SanKantSuStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.25f : 0.5f;
    }

    public class SanKantSuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SanKantSuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // 공격시 +-20도 이내에 50% 느린 추가 탄환 3개
            if (infos[0] is not BulletInfo info) return;
            double angle = HolderStat.TowerInfo is CompleteTowerInfo ? 20d : 30d;
            var rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                float targetAngle = (float)(rand.NextDouble() * (angle * 2) - angle); // -20f ~ 20f
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle),
                    info.SpeedMultiplier / 2f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            }
        }
    }

    public class SanKantSuImageOption : TowerImageOption
    {
        public override string Name => nameof(SanKantSuImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (21, 7) };
    }
}
