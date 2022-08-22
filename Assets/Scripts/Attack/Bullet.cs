using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        private readonly int bulletSpeed = 10; //BulletAttackBehaviour.cs 파일의 bulletSpeed와 같은 값으로 유지
        private GameObject effect;
        public BulletInfo BulletInfo => (BulletInfo)attackInfo;

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        private void Update()
        {
            OnUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;

            if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

            enemy.OnHit(BulletInfo, out bool critical, BulletInfo.ShooterTowerStat.InstantDeath);

            if (critical)
            {
                effect = ResourceDictionary.Get<GameObject>("Prefabs/CriticalPrefab");
                Instantiate(effect, transform.position, Quaternion.identity);
                SoundManager.Inst.PlayBullet("HitCriticalBullet");
            }
            else
            {
                effect = ResourceDictionary.Get<GameObject>("Prefabs/BoomPrefab");
                Instantiate(effect, transform.position, Quaternion.identity);
                SoundManager.Inst.PlayBullet("HitNormalBullet", 0.7f);
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
                    UpdateTarget(30f, collision.gameObject.GetComponent<EnemyController>());
                    break;
                case 4:
                    UpdateTarget(80f, collision.gameObject.GetComponent<EnemyController>());
                    break;
            }

            if (BulletInfo.CurrentPenetrateCount == BulletInfo.PenetrateLevel)
            {
                Destroy(gameObject);
                return;
            }

            ++BulletInfo.CurrentPenetrateCount;
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        protected virtual void OnFixedUpdate()
        {
            transform.position += bulletSpeed * 0.02f * BulletInfo.Direction * BulletInfo.SpeedMultiplier *
                                  RoundManager.Inst.playSpeed * RoundManager.Inst.optionOnOff;
        }
        protected virtual void OnUpdate()
        {
            /*
            transform.position += bulletSpeed * Time.deltaTime * BulletInfo.Direction * BulletInfo.SpeedMultiplier *
                                  RoundManager.Inst.playSpeed * RoundManager.Inst.optionOnOff;
            */
        }

        protected override void OnInit()
        {
            float additionalSize = Mathf.Min((BulletInfo.Damage - 10f) / 1000f,1);
            transform.localScale += new Vector3(additionalSize, additionalSize, 0);
            if (BulletInfo.CannonOverride)
                BulletInfo.Direction = Vector3.up;
            
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
                float sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag >= minDistance || enemy == currentEnemy /* When enemy is current Target */) continue;

                float angle = Vector3.Angle(BulletInfo.Direction, (enemy.transform.position - pos).normalized);
                if (angle > maxAngle || angle < -maxAngle) continue;

                minDistance = sqrMag;
                proxTeki = enemy;
            }

            if (proxTeki == null) return;

            var targetVector = MathHelper.ExpectedLocation(pos, bulletSpeed * BulletInfo.SpeedMultiplier,
                proxTeki.transform.position, proxTeki.GetSpeed);
            BulletInfo.Direction = (targetVector - pos).normalized;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, MathHelper.GetAngle(Vector3.up, BulletInfo.Direction) * 2));
        }
    }
}
