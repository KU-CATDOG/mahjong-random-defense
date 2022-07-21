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
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/ExplosionPrefab")).GetComponent<Explosive>();
            ExplosiveInfo info = new ExplosiveInfo(enemy.transform.position,radius,enemy,towerStat,enemy.transform.position,"");
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
}
