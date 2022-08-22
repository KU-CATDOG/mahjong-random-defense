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
        public override int Priority => 98;

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;
            int hornCount = RoundManager.Inst.RelicManager[typeof(HornRelic)];
            if (HolderStat.TowerInfo is not CompleteTowerInfo)
            {
                float targetAngle = new Random().Next(2) > 0 ? -30f : 30f;
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                    infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier * 0.8f,
                        info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                return;
            }

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
        }
    }

    public class ToiToiImageOption : TowerImageOption
    {
        public override string Name => nameof(ToiToiImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (20, 1) };
    }
}
