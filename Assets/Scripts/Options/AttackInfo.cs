using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public enum AttackImage
    {
        Default = 1,
        Sou = 2,
        Pin = 4,
        Wan = 8,
        SSDG = 16,
        Cannon = 32,
        Missile = 64,
    }
    public enum TargetTo
    {
        Proximity = 1,
        HighestHp = 2,
        Random = 4,
    }
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

        public AttackImage ImageName { get; private set; } = AttackImage.Default;

        private int imagePriority = 0;

        public float ShootDelay { get; }

        private List<AttackOnHitOption> onHitOptions = new();
        public IReadOnlyList<AttackOnHitOption> OnHitOptions => onHitOptions;

        protected AttackInfo(TowerStat shooterTowerStat, Vector3 startPosition, float shootDelay)
        {
            StartPosition = startPosition;
            ShooterTowerStat = shooterTowerStat;
            ShootDelay = shootDelay;
        }

        /// <summary>
        /// 삭, 통, 만에 대해 haiType를 AttackImage로 자동 변환하여 BulletImage 설정
        /// </summary>
        public void SetImage(HaiType haiType, int priority)
        {
            AttackImage translation = haiType switch {
                HaiType.Sou => AttackImage.Sou,
                HaiType.Pin => AttackImage.Pin,
                HaiType.Wan => AttackImage.Wan,
                _ => AttackImage.Default,
            };
            SetImage(translation, priority);
        }
        public void SetImage(AttackImage name, int priority)
        {
            if (priority > imagePriority)
            {
                ImageName = name;
            }
        }

        public void AddOnHitOption(AttackOnHitOption onHitOption)
        {
            onHitOptions.Add(onHitOption);
        }
        public void UpdateShupaiLevel(HaiType type, int level) 
        {
            switch(type)
            {
                case HaiType.Sou:
                    if(this is not BulletInfo bulletInfo) return;
                    if(bulletInfo.PenetrateLevel >= level) return;
                    bulletInfo.PenetrateLevel = level;
                    break;
                case HaiType.Pin:
                    foreach(AttackOnHitOption option in onHitOptions) {
                        if(option is PinOnHitOption pinOption){
                            pinOption.Level = level; return;
                        }
                    }
                    onHitOptions.Add(new PinOnHitOption(level));
                    break;
                case HaiType.Wan:
                    foreach(AttackOnHitOption option in onHitOptions) {
                        if(option is WanOnHitOption wanOption){
                            wanOption.Level = level; return;
                        }
                    }
                    onHitOptions.Add(new WanOnHitOption(level));
                    break;
            }
        }
    }

    public class BulletInfo : AttackInfo
    {
        public override AttackType AttackType => AttackType.Bullet;
        public TargetTo TargetTo { get; set; }

        public Vector3 Direction { get; set; }

        public float SpeedMultiplier { get; set; }

        public int PenetrateLevel { get; set; }

        public int CurrentPenetrateCount { get; set; }

        public float DamageMultiplier { get; set; } = 1f;

        public BulletInfo(Vector3 direction, float speedMultiplier,
            TowerStat towerStat, Vector3 startPosition, AttackImage imageName, float shootDelay, TargetTo targetTo = TargetTo.Proximity)
            : base(towerStat, startPosition, shootDelay)
        {
            SpeedMultiplier = speedMultiplier;
            Direction = direction;
            TargetTo = targetTo;
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
            : base(towerStat, startPosition, shootDelay)
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
            : base(towerStat, startPosition, shootDelay)
        {
            Target = target;
            TargetPosition = targetPosition;
        }
    }
}