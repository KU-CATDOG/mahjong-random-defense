using UnityEngine;

namespace MRD
{
    internal class Grenade : Bullet
    {
        public EnemyController originEnemy;
        private readonly float targetTime = 1f;
        private float timer;

        protected override void OnInit()
        {
        }

        protected override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if (timer > targetTime)
            {
                var tmp = Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/ExplosionPrefab"))
                    .GetComponent<Explosive>();
                var info = new ExplosiveInfo(transform.position,
                    0.5f + BulletInfo.ShooterTowerStat.TowerInfo.Hais.Count * 0.1f, originEnemy,
                    BulletInfo.ShooterTowerStat, transform.position, "", 2);
                tmp.Init(info);
                Destroy(gameObject);
                return;
            }

            transform.position = BulletInfo.StartPosition + BulletInfo.Direction * EaseOutCubic(timer / targetTime);
            // transform.Rotate(Vector3.up, Time.deltaTime * RoundManager.Inst.playSpeed * 360f / targetTime);
        }

        private float EaseOutCubic(float t) // 0~1
            =>
                1 - Mathf.Pow(1 - t, 3);
    }
}
