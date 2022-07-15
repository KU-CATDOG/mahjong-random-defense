using System.Collections.Generic;

namespace MRD
{
    public class SanAnKouStatOption : TowerStatOption
    {
        public override string Name => nameof(SanAnKouStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 30.0f : 0.0f;

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.5f : 0.1f;

    }
    public class SanAnKouOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SanAnKouOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -15f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 15f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
        }
    }
}
