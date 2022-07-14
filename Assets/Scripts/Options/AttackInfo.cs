using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public enum AttackType
    {
        Bullet = 1,
        Explosive = 2,
        Blade = 4,
    }

    public abstract class AttackInfo
    {
        public abstract AttackType AttackType { get; }

        public TowerStat ShooterTowerStat { get; }

        public Vector3 StartPosition { get; }

        public string ImageName { get; }

        public float ShootDelay { get; }

        public IReadOnlyList<AttackOnHitOption> OnHitOptions { get; }

        protected AttackInfo(TowerStat shooterTowerStat, Vector3 startPosition, string imageName, float shootDelay)
        {
            StartPosition = startPosition;
            ShooterTowerStat = shooterTowerStat;
            ImageName = imageName;
            ShootDelay = shootDelay;
        }
    }

    public class BulletInfo : AttackInfo
    {
        public override AttackType AttackType => AttackType.Bullet;

        public Vector3 Direction { get; }

        public float SpeedMultiplier { get; }

        public BulletInfo(Vector3 direction, float speedMultiplier,
            TowerStat towerStat, Vector3 startPosition, string imageName, float shootDelay)
            : base(towerStat, startPosition, imageName, shootDelay)
        {
            SpeedMultiplier = speedMultiplier;
            Direction = direction;
        }
    }

    public class ExplosiveInfo : AttackInfo
    {
        public override AttackType AttackType => AttackType.Explosive;

        public Vector3 Origin { get; }

        public float Radius { get; }

        public EnemyController Target { get; }


        public ExplosiveInfo(Vector3 origin, float radius, EnemyController target,
            TowerStat towerStat, Vector3 startPosition, string imageName, float shootDelay = 0)
            : base(towerStat, startPosition, imageName, shootDelay)
        {
            Target = target;
            Origin = origin;
            Radius = radius;
        }
    }

    public class BladeInfo : AttackInfo
    {
        public override AttackType AttackType => AttackType.Blade;

        public EnemyController Target { get; }

        public Vector3 TargetPosition { get; }


        public BladeInfo(EnemyController target, Vector3 targetPosition,
            TowerStat towerStat, Vector3 startPosition, string imageName, float shootDelay = 0)
            : base(towerStat, startPosition, imageName, shootDelay)
        {
            Target = target;
            TargetPosition = targetPosition;
        }
    }
}