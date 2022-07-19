using System.Collections.Generic;

namespace MRD
{
    public class ShuAnKouStatOption : TowerStatOption
    {
        public override string Name => nameof(ShuAnKouStatOption);

        public override float AdditionalAttack => 100.0f;
        public override float AdditionalAttackSpeedMultiplier => 1.5f;

    }
    public class ShuAnKouOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ShuAnKouOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -10f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 10f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -20f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 20f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
        }
    }
}
