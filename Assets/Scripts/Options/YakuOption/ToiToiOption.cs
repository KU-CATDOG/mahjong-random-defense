using System.Collections.Generic;

namespace MRD
{
    public class ToiToiStatOption : TowerStatOption
    {
        public override string Name => nameof(ToiToiStatOption);

        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.2f : 0.05f;
    }
    public class ToiToiOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ToiToiOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay));
        }
    }
    public class ToiToiImageOption : TowerImageOption
    {
        public override string Name => nameof(ToiToiImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (20, 0) };
    }
}
