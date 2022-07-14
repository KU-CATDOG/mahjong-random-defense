using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        public BulletInfo BulletInfo => (BulletInfo)attackInfo;

        private void Update()
        {
            transform.position += BulletInfo.Direction * BulletInfo.SpeedMultiplier;
        }

        protected override void OnInit()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;

            if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

            foreach (var o in BulletInfo.OnHitOptions)
            {
                o.OnHit(enemy);
            }

            enemy.OnHit(BulletInfo);
            Destroy(gameObject);
        }
    }
}