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

        public override int Priority => 99;

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            if (infos[0] is BulletInfo info) 
            {
                int hornCount = RoundManager.Inst.RelicManager[typeof(HornRelic)];

                foreach(int angle in new int[] {-30, -20, -10, 10, 20, 30})
                {
                    infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, angle), info.SpeedMultiplier,
                    info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                    if (UnityEngine.Random.Range(0f, 1f) < hornCount * .25f)
                        infos.Add(new BulletInfo(MathHelper.RotateVector(info.Direction, 10f), info.SpeedMultiplier * 0.8f,
                        info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay, info.Damage, penetrateLevel: info.PenetrateLevel));
                }
            } 
            else if(infos[0] is BladeInfo bladeInfo)
            {
                foreach(var i in infos.Cast<BladeInfo>())
                {
                    i.DamageMultiplier *= 4f;
                }
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
