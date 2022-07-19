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
        private int attackCount = 0;
        public override string Name => nameof(IlGiTongGwanOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: UpdateShupaiLevel 테스트 필요
            // TODO: imageName == "Cannon"에 해당하는 Asset 필요
            // 10회 공격시 마다 공격 대신 전방으로 대포알 발사. 모든 적을 관통하며 x2 피해. 해당 수패의 3단계 효과를 가짐. 비멘젠이면 2단계
            bool isMenzen = ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos.All(x => x.IsMenzen);
            bool isComplete = HolderStat.TowerInfo is CompleteTowerInfo;

            var (targetAttackCount, targetLevel) = (isMenzen, isComplete) switch {
                (false, false) => (15, 1),
                (false, true) => (10, 2),
                (true, false) => (10, 1),
                (true, true) => (10, 3),
            };
            
            if(attackCount++ < targetAttackCount) return;
            if (infos[0] is not BulletInfo info) return;
            infos.RemoveAt(0);
            
            var cannon = new BulletInfo(info.Direction, info.SpeedMultiplier, info.ShooterTowerStat, info.StartPosition, "Cannon", info.ShootDelay);
            cannon.PenetrateLevel = 2;

            var haiType = ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos
                    .Where(x => x is ShuntsuInfo)
                    .Cast<ShuntsuInfo>().GroupBy(x => x.HaiType)
                    .Where(g => g.Count() > 2)
                    .First().Key;
            cannon.UpdateShupaiLevel(haiType, targetLevel);
            infos.Add(cannon);
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
