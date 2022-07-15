using UnityEngine;

namespace MRD
{
    public class ExplosiveOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/ExplosionPrefab")).GetComponent<Explosive>();
            tmp.Init(AttackInfo);
        }
    }
    public class BladeOnHitOption : AttackOnHitOption
    {
        public override string Name => nameof(WanOnHitOption);

        public override void OnHit(EnemyController enemy)
        {
            var tmp = Object.Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/Blade")).GetComponent<Blade>();
            tmp.Init(AttackInfo);
        }
    }
}
