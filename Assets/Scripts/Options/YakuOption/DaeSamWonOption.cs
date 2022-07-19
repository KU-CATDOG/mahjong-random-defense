using System.Collections.Generic;

namespace MRD
{
    public class DaeSamWonStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeSamWonStatOption);

        public override float AdditionalAttackMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 3.0f : 2.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.3f : 0.5f;

    }
    public class DaeSamWonOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeSamWonOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: 폭발 반지름 3m 구현
            // 무제한 관통되는 느린 미사일 발사. 접촉 시마다 반지름 3m의 거대한 폭발
            if(infos[0] is not BulletInfo bulletInfo) return;
            infos.RemoveAt(0);

            var missile = new BulletInfo(bulletInfo.Direction, bulletInfo.SpeedMultiplier,
                bulletInfo.ShooterTowerStat, bulletInfo.StartPosition, "Missile", bulletInfo.ShootDelay, TargetTo.HighestHp);
            missile.AddOnHitOption(new ExplosiveOnHitOption());
            infos.Add(missile);
        }
    }
}
