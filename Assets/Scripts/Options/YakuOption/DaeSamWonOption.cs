using System.Collections.Generic;

namespace MRD
{
    public class DaeSamWonStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeSamWonStatOption);

        public override Stat AdditionalStat => new
    (
            damageMultiplier: HolderStat.TowerInfo is CompleteTowerInfo ? 3.0f : 2.0f,
            attackSpeed: HolderStat.TowerInfo is CompleteTowerInfo ? 0.3f : 0.5f
    );

        public override TargetTo TargetTo => TargetTo.HighestHp;
    }

    public class DaeSamWonOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeSamWonOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            // TODO: 중형타워
            // 무제한 관통되는 느린 미사일 발사. 접촉 시마다 반지름 3m의 거대한 폭발
            if (infos[0] is not BulletInfo bulletInfo) return;
            if (HolderStat.TowerInfo is not CompleteTowerInfo) return;

            infos.RemoveAt(0);

            var missile = new BulletInfo(bulletInfo.Direction, bulletInfo.SpeedMultiplier,
                bulletInfo.ShooterTowerStat, bulletInfo.StartPosition, AttackImage.Missile, bulletInfo.ShootDelay,
                bulletInfo.Damage);
            missile.AddOnHitOption(new ExplosiveOnHitOption(HolderStat, 3f));
            infos.Add(missile);
        }
    }
    public class DaeSamWonImageOption : TowerImageOption
    {
        public override string Name => nameof(DaeSamWonImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (34, 1) };
    }
}
