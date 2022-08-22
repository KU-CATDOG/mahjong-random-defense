using System;
using System.Collections.Generic;

namespace MRD
{
    public class SanAnKouStatOption : TowerStatOption
    {
        public override string Name => nameof(SanAnKouStatOption);

        public override Stat AdditionalStat => new
    (
            damageConstant: HolderStat.TowerInfo is CompleteTowerInfo ? 30.0f : 0.0f,
            damagePercent: HolderStat.TowerInfo is CompleteTowerInfo ? 0.5f : 0.4f
    );
    }

    public class SanAnKouOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SanAnKouOption);
        public override int Priority => 99;
        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;
            int hornCount = RoundManager.Inst.RelicManager[typeof(HornRelic)];
            if (HolderStat.TowerInfo is not CompleteTowerInfo)
            {
                float targetAngle = new Random().Next(2) > 0 ? -15f : 15f;
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                    infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, targetAngle), info.SpeedMultiplier * 0.8f,
                        info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                
                return;
            }

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -15f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 15f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -15f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
            if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 15f), info.SpeedMultiplier * info.SpeedMultiplier * 0.8f,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
        }
    }

    public class SanAnKouImageOption : TowerImageOption
    {
        public override string Name => nameof(SanAnKouImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (21, 7) };
    }
}
