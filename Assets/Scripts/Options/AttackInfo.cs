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
        Grenade = 128,
        Blade = 256,
        Minitower = 512,
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
        Minitower = 8,
    }

    public abstract class AttackInfo
    {
        private int imagePriority = -1;

        private readonly List<AttackOnHitOption> onHitOptions = new();

        protected AttackInfo(TowerStat shooterTowerStat, Vector3 startPosition, float shootDelay)
        {
            StartPosition = startPosition;
            ShooterTowerStat = shooterTowerStat;
            ShootDelay = shootDelay;
        }

        public abstract AttackType AttackType { get; }

        public TowerStat ShooterTowerStat { get; }

        public Vector3 StartPosition { get; }

        public AttackImage ImageName { get; private set; } = AttackImage.Default;

        public float ShootDelay { get; }
        public IReadOnlyList<AttackOnHitOption> OnHitOptions => onHitOptions;

        /// <summary>
        ///     삭, 통, 만에 대해 haiType를 AttackImage로 자동 변환하여 BulletImage 설정
        /// </summary>
        public void SetImage(HaiType haiType, int priority)
        {
            var translation = haiType switch
            {
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
                imagePriority = priority;
            }
        }

        public void AddOnHitOption(AttackOnHitOption onHitOption)
        {
            onHitOptions.Add(onHitOption);
        }

        public void UpdateShupaiLevel(HaiType type, int level)
        {
            if (this is not BulletInfo bulletInfo) return;
            bulletInfo.SetImage(type, level);
            switch (type)
            {
                case HaiType.Sou:
                    if (bulletInfo.PenetrateLevel >= level) return;
                    bulletInfo.PenetrateLevel = level;
                    break;
                case HaiType.Pin:
                    foreach (var option in onHitOptions)
                    {
                        if (option is PinOnHitOption pinOption)
                        {
                            pinOption.Level = level;
                            return;
                        }
                    }

                    onHitOptions.Add(new PinOnHitOption(level));
                    break;
                case HaiType.Wan:
                    foreach (var option in onHitOptions)
                    {
                        if (option is WanOnHitOption wanOption)
                        {
                            wanOption.Level = level;
                            return;
                        }
                    }

                    onHitOptions.Add(new WanOnHitOption(level));
                    break;
            }
        }
        public void UpgradeShupaiLevel(HaiType type)
        {
            if (this is not BulletInfo bulletInfo) return;
            switch (type)
            {
                case HaiType.Sou:
                    bulletInfo.PenetrateLevel = bulletInfo.PenetrateLevel+1 > 4 ? 4 : bulletInfo.PenetrateLevel+1;
                    break;
                case HaiType.Pin:
                    foreach (var option in onHitOptions)
                    {
                        if (option is PinOnHitOption pinOption)
                        {
                            pinOption.Level = pinOption.Level + 1 > 4 ? 4 : pinOption.Level+1;
                            return;
                        }
                    }
                    onHitOptions.Add(new PinOnHitOption(1));
                    break;
                case HaiType.Wan:
                    foreach (var option in onHitOptions)
                    {
                        if (option is WanOnHitOption wanOption)
                        {
                            wanOption.Level = wanOption.Level + 1 > 4 ? 4 : wanOption.Level+1;
                            return;
                        }
                    }
                    onHitOptions.Add(new WanOnHitOption(1));
                    break;
            }
        }
    }

    public class BulletInfo : AttackInfo
    {
        public BulletInfo(Vector3 direction, float speedMultiplier,
            TowerStat towerStat, Vector3 startPosition, AttackImage imageName, float shootDelay, float damage,
            bool forceImage = false)
            : base(towerStat, startPosition, shootDelay)
        {
            SpeedMultiplier = speedMultiplier;
            Direction = direction;
            Damage = damage;
            SetImage(imageName, forceImage ? 5 : 0);
        }

        public override AttackType AttackType => AttackType.Bullet;

        public Vector3 Direction { get; set; }

        public float SpeedMultiplier { get; set; }

        public int PenetrateLevel { get; set; }

        public int CurrentPenetrateCount { get; set; }

        public float Damage { get; set; }
    }

    public class ExplosiveInfo : AttackInfo
    {
        public ExplosiveInfo(Vector3 origin, float radius, EnemyController target,
            TowerStat towerStat, Vector3 startPosition, string imageName, int type, float shootDelay = 0, bool extraBomb = false, int extraBombCount = 4, bool isJangpan = false, float damageMultiplier = 1.0f)
            : base(towerStat, startPosition, shootDelay)
        {
            Target = target;
            Origin = origin;
            Radius = radius;
            Type = type;
            ExtraBomb = extraBomb;
            ExtraBombCount = extraBombCount;
            DamageMultiplier = isJangpan ? 0.2f : 0.5f;
            DamageMultiplier *= damageMultiplier;
        }

        public override AttackType AttackType => AttackType.Explosive;

        public Vector3 Origin { get; }

        public float Radius { get; }

        public EnemyController Target { get; }

        public int Type { get; }

        public bool ExtraBomb { get; }
        public int ExtraBombCount { get; }
        public float DamageMultiplier { get; }
    }

    public class BladeInfo : AttackInfo
    {
        public BladeInfo(EnemyController target, Vector3 targetPosition,
            TowerStat towerStat, Vector3 startPosition, AttackImage imageName, float shootDelay = 0,
            bool damageToTarget = false, float damageMultiplier = 1.0f)
            : base(towerStat, startPosition, shootDelay)
        {
            Target = target;
            TargetPosition = targetPosition;
            this.damageToTarget = damageToTarget;
            SetImage(imageName, 0);
            DamageMultiplier = 1.5f * damageMultiplier;
        }

        public override AttackType AttackType => AttackType.Blade;

        public EnemyController Target { get; }

        public Vector3 TargetPosition { get; }

        public bool damageToTarget { get; }

        public float DamageMultiplier { get; }
    }
    public class MinitowerInfo : AttackInfo
    {
        public MinitowerInfo(TowerStat towerStat, Vector3 startPosition, AttackImage imageName, float shootDelay = 0)
            : base(towerStat, startPosition, shootDelay)
        {
            TowerStat = towerStat;
            SetImage(imageName, 0);
        }

        public override AttackType AttackType => AttackType.Minitower;        

        public TowerStat TowerStat { get; }

        public float Damage { get; set; }
    }
}
