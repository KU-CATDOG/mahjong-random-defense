using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ShuAnKouStatOption : TowerStatOption
    {
        public override string Name => nameof(ShuAnKouStatOption);
        public override Stat AdditionalStat => new Stat
        (
            
        );
    }

    public class ShuAnKouOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(ShuAnKouOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is not BulletInfo info) return;
            int hornCount = RoundManager.Inst.RelicManager[typeof(HornRelic)];

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -10f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 10f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -20f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 20f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));

            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            for(int i=0;i<hornCount;i++) {
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -10f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 10f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));

                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -20f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 20f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));

                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, -30f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
                infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 30f), info.SpeedMultiplier* 0.8f - 0.2f*i,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage));
            }
        }
    }
    public class ShuAnKouImageOption : TowerImageOption
    {
        public override string Name => nameof(ShuAnKouImageOption);
        protected override List<(int index, int order)> completeTowerImages
        {
            get
            {
                var ret = new List<(int index, int order)>() { (40, 2)};
                if (((YakuHolderInfo)HolderStat.TowerInfo).YakuList.All(x => x.Name is "ShuAnKou")) ret.Add((39, 1));
                return ret;
            }
        }
    }
}
