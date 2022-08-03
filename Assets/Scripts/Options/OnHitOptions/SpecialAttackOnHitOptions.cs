using UnityEngine;
using System.Linq;
namespace MRD
{
    public class ExplosiveOnHitOption : AttackOnHitOption
    {
        private readonly float radius;

        private readonly TowerStat towerStat;

        public ExplosiveOnHitOption(TowerStat towerStat, float radius, float damageMultiplier = 1.0f)
        {
            this.towerStat = towerStat;
            this.radius = radius;
        }

        public override string Name => nameof(WanOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/ExplosionPrefab"))
                .GetComponent<Explosive>();
            var colorList = towerStat.TowerInfo.Hais.Where(x => x.Spec.HaiType == HaiType.Sangen).GroupBy(x => x.Spec.Number).Select(x =>x.Key).ToList();

            var info = new ExplosiveInfo(enemy.transform.position, radius, enemy, towerStat, enemy.transform.position,
                "", colorList[UnityEngine.Random.Range(0, colorList.Count)]);
            tmp.Init(info);
        }
    }

    public class BladeOnHitOption : AttackOnHitOption
    {
        private readonly TowerStat towerStat;

        public BladeOnHitOption(TowerStat towerStat, float damageMultiplier = 1.0f) => this.towerStat = towerStat;

        public override string Name => nameof(WanOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Blade")).GetComponent<Blade>();
            var info = new BladeInfo(enemy, enemy.transform.position, towerStat, enemy.transform.position,
                AttackImage.Blade);
            tmp.Init(info);
        }
    }

    public class GrenadeOnHitOption : AttackOnHitOption
    {
        private readonly TowerStat towerStat;

        public GrenadeOnHitOption(TowerStat towerStat) => this.towerStat = towerStat;

        public override string Name => nameof(GrenadeOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Bullets/Grenade"))
                .GetComponent<Grenade>();
            tmp.originEnemy = enemy;
            float targetAngle = Random.Range(-180, 180);
            var bulletInfo = new BulletInfo(MathHelper.RotateVector(Vector3.up, targetAngle), 1, towerStat,
                enemy.transform.position, AttackImage.Grenade, 0, 0);
            tmp.Init(bulletInfo);
        }
    }
    public class JangpanOnHitOption : AttackOnHitOption
    {
        private readonly TowerStat towerStat;
        private float radius;

        public JangpanOnHitOption(TowerStat towerStat, float radius) {
             this.towerStat = towerStat;
             this.radius = radius;
        }

        public override string Name => nameof(JangpanOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Jangpan"))
                .GetComponent<Jangpan>();
            var info = new ExplosiveInfo(enemy.transform.position, radius, enemy, towerStat, enemy.transform.position,
                "", towerStat.TowerInfo.Hais[0].Spec.Number, isJangpan: true);
            tmp.Init(info);
        }
    }
}
