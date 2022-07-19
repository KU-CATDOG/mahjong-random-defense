using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class IlGiTongGwanStatOption : TowerStatOption
    {
        public override string Name => nameof(IlGiTongGwanStatOption);

    }
    public class IlGiTongGwanOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(IlGiTongGwanOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // WIP
            // 10회 공격시 마다 공격 대신 전방으로 대포알 발사. 모든 적을 관통하며 x2 피해. 해당 수패의 3단계 효과를 가짐. 비멘젠이면 2단계
            if (infos[0] is not BulletInfo info) return;
            infos.RemoveAt(0);
            
            // TODO : Create Seperate obejct "Cannon" for full implementation.
            var merPo = new BulletInfo(info.Direction, info.SpeedMultiplier*2, info.ShooterTowerStat, info.StartPosition, info.ImageName, info.ShootDelay);
            infos.Add(merPo);
        }
    }
    public class IlGiTongGwanImageOption : TowerImageOption
    {
        public override string Name => nameof(IlGiTongGwanImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get => ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos
                    .Where(x => x is ShuntsuInfo)
                    .Cast<ShuntsuInfo>().GroupBy(x => x.HaiType)
                    .Where(g => g.Count() > 2)
                    .First().Key switch
            {
                HaiType.Wan => new() { (9, 2) },
                HaiType.Pin => new() { (10, 2) },
                HaiType.Sou => new() { (11, 2) },
                _ => new() { },
            };
        }
    }
}
