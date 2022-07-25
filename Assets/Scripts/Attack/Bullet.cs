using UnityEngine;

namespace MRD
{
    
    public class Bullet : Attack
    {
        private int bulletSpeed = 10;//BulletAttackBehaviour.cs 파일의 bulletSpeed와 같은 값으로 유지
        public BulletInfo BulletInfo => (BulletInfo)attackInfo;
        private GameObject effect;

        private void Update()
        {
            OnUpdate();
        }
        protected virtual void OnUpdate()
        {
            transform.position += bulletSpeed * Time.deltaTime * BulletInfo.Direction * BulletInfo.SpeedMultiplier * RoundManager.Inst.playSpeed;
            if(!((-2 < transform.position.x && transform.position.x < 12) || (-2 < transform.position.y && transform.position.y < 18)))
                Destroy(gameObject);
        }

        protected override void OnInit()
        {
            var additionalSize = ((BulletInfo.Damage - 10f )/ 1000f);
            transform.localScale += new Vector3(additionalSize, additionalSize, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;

            if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

            enemy.OnHit(BulletInfo, out bool critical);

            if (critical)
            {
                effect = ResourceDictionary.Get<GameObject>("Prefabs/CriticalPrefab");
                Instantiate(effect, transform.position, Quaternion.identity);
            }
            else
            {
                effect = ResourceDictionary.Get<GameObject>("Prefabs/BoomPrefab");
                Instantiate(effect, transform.position, Quaternion.identity);
            }
                

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
            var enemyList = RoundManager.Inst.Spawner.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = transform.position;

            float minDistance = 1000000000f;

            EnemyController proxTeki = null;

            foreach (var enemy in enemyList)
            {
                var sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag >= minDistance || enemy == currentEnemy /* When enemy is current Target */) continue;
                
                float angle = Vector3.Angle(BulletInfo.Direction, (enemy.transform.position - pos).normalized);
                // Debug.Log("angle: " + angle);
                if (angle > maxAngle || angle < -maxAngle) continue;

                minDistance = sqrMag;
                proxTeki = enemy;
            }

            if (proxTeki == null) return;

            var targetVector = BulletAttackBehaviour.ExpectedLocation(pos, bulletSpeed * BulletInfo.SpeedMultiplier, proxTeki.transform.position, proxTeki.GetSpeed);
            BulletInfo.Direction = (targetVector - pos).normalized;
        }

        private static float GetAngle (Vector3 vStart, Vector3 vEnd)
        {
            Vector3 v = vEnd - vStart;
    
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }
    }
}