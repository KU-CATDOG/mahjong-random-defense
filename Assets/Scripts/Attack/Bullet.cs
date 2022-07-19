using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        
        public BulletInfo BulletInfo => (BulletInfo)attackInfo;

        private void Update()
        {
            transform.position += 5 * Time.deltaTime * BulletInfo.Direction * BulletInfo.SpeedMultiplier;
        }

        protected override void OnInit()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;

            if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
            
            foreach (var o in BulletInfo.OnHitOptions)
            {
                o.OnHit(enemy);
            }

            enemy.OnHit(BulletInfo);

            switch (BulletInfo.PenetrateLevel)
            {
                case 0:
                case 1:
                    if (BulletInfo.CurrentPenetrateCount == BulletInfo.PenetrateLevel)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    break;
                case 2:
                    return;
                case 3:
                    UpdateTarget(30f,collision.gameObject.GetComponent<EnemyController>());
                    break;
                case 4:
                    UpdateTarget(80f,collision.gameObject.GetComponent<EnemyController>());
                    break;
            }

            if (BulletInfo.CurrentPenetrateCount == BulletInfo.PenetrateLevel)
            {
                Destroy(gameObject);
                return;
            }

            ++BulletInfo.CurrentPenetrateCount;
        }

        private void UpdateTarget(float maxAngle, EnemyController currentEnemy)
        {
            var enemyList = RoundManager.Inst.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = transform.position;

            float minDistance = 1000000000f;

            EnemyController proxTeki = null;

            foreach (var enemy in enemyList)
            {
                var sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag >= minDistance || enemy == currentEnemy /* When enemy is current Target */) continue;
                
                float angle = Vector3.Angle(BulletInfo.Direction, (enemy.transform.position - pos).normalized);
                Debug.Log("angle: " + angle);
                if (angle > maxAngle || angle < -maxAngle) continue;

                minDistance = sqrMag;
                proxTeki = enemy;
            }

            if (proxTeki == null) return;

            var targetVector = BulletAttackBehaviour.ExpectedLocation(pos, 5*BulletInfo.SpeedMultiplier, proxTeki.transform.position, proxTeki.GetSpeed);
            BulletInfo.Direction = (targetVector - pos).normalized;
        }

        private static float GetAngle (Vector3 vStart, Vector3 vEnd)
        {
            Vector3 v = vEnd - vStart;
    
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }
    }
}