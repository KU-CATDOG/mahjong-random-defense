using System;
using System.Collections.Generic;

namespace MRD
{
    public class ToiToiStatOption : TowerStatOption
    {
        public override string Name => nameof(ToiToiStatOption);
        public override Stat AdditionalStat => new Stat
    (
        damagePercent: HolderStat.TowerInfo is CompleteTowerInfo ? 0.2f : 0.05f
    );
    }

    public class ToiToiOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ToiToiOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;
            int hornCount = RoundManager.Inst.RelicManager[typeof(HornRelic)];
            if (HolderStat.TowerInfo is not CompleteTowerInfo)
            {
                float targetAngle = new Random().Next(2) > 0 ? -30f : 30f;
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                
                for(int i=0;i<hornCount;i++)
                    infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier * 0.8f - 0.2f*i,
                        info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                return;
            }

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            for(int i=0;i<hornCount;i++) {
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            }
        }
    }

    public class ToiToiImageOption : TowerImageOption
    {
        public override string Name => nameof(ToiToiImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (20, 1) };
    }
}
