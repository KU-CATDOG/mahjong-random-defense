using UnityEngine;

namespace MRD
{
    public class ExplosiveOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        private TowerStat towerStat;
        private float radius;
        public ExplosiveOnHitOption(TowerStat towerStat, float radius)
        {
            this.towerStat = towerStat;
            this.radius = radius;
        }
        public override void OnHit(EnemyController enemy)
        {
            // TODO: 백발중 타입 체크 방식 수정 필요
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/ExplosionPrefab")).GetComponent<Explosive>();
            ExplosiveInfo info = new ExplosiveInfo(enemy.transform.position,radius,enemy,towerStat,enemy.transform.position,"",towerStat.TowerInfo.Hais[0].Spec.Number);
            tmp.Init(info);
        }
    }
    public class BladeOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);
        private TowerStat towerStat;
        public BladeOnHitOption(TowerStat towerStat)
        {
            this.towerStat = towerStat;
        }
        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Blade")).GetComponent<Blade>();
            BladeInfo info = new BladeInfo(enemy, enemy.transform.position, towerStat,enemy.transform.position,"");
            tmp.Init(info);
        }
    }
    public class GrenadeOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(GrenadeOnHitOption);
        private TowerStat towerStat;
        public GrenadeOnHitOption(TowerStat towerStat)
        {
            this.towerStat = towerStat;
        }
        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Bullets/Grenade")).GetComponent<Grenade>();
            float targetAngle = Random.Range(-180,180);
            var bulletInfo = new BulletInfo(MathHelper.RotateVector(Vector3.up,targetAngle), 1, towerStat, enemy.transform.position, AttackImage.Grenade, 0, 0);
            tmp.Init(bulletInfo);
        }
    }
    
}
