using System.Collections;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class BulletAttackBehaviour : AttackBehaviour
    {
        private readonly float bulletSpeed = 10f; //Bullet.cs 파일의 bulletSpeed와 같은 값으로 유지
        private AttackImage defaultAttackImage = AttackImage.Default;
        private float timer;

        public override void OnInit()
        {
            if (Tower.TowerStat.TowerInfo != null)
                defaultAttackImage = Tower.TowerStat.TowerInfo.DefaultAttackImage;
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;

            if (timer < 0.35f / Tower.TowerStat.FinalAttackSpeed) return;

            var enemyList = RoundManager.Inst.Spawner.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = Tower.transform.position;
            EnemyController targetTeki = null;
            switch (Tower.TowerStat.TargetTo)
            {
                case TargetTo.Proximity:
                    float minDistance = 1000000000f;
                    foreach (var enemy in enemyList)
                    {
                        float sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                        if (sqrMag > 144f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;

                        minDistance = sqrMag;
                        targetTeki = enemy;
                    }

                    break;
                case TargetTo.HighestHp:
                    float maxHealth = 0f;
                    foreach (var enemy in enemyList)
                    {
                        float sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                        if (sqrMag > 144f || enemy.Health < maxHealth) continue;

                        targetTeki = enemy;
                        maxHealth = enemy.Health;
                    }

                    break;
                case TargetTo.Spree:
                    timer = 0f;
                    Attack(null);
                    return;
                default: // Random
                    var enemyInRange = enemyList.Where(enemy => (pos - enemy.transform.position).sqrMagnitude <= 144f)
                        .ToList();
                    if (enemyInRange.Count <= 0) return;

                    int index = Random.Range(0, enemyInRange.Count);
                    targetTeki = enemyList[index];
                    break;
            }

            if (targetTeki == null)
                return;


            timer = 0f;
            Attack(targetTeki);
        }

        private void Attack(EnemyController enemy)
        {
            // FIXME: ExpectedLocation이 bulletinfo를 처리하기 전에 계산되어 일부 option들이 제대로 작동하지 않을 수 있음
            BulletInfo bulletInfo;
            if(enemy == null) { // 난사
                var direction = MathHelper.RotateVector(Vector3.up, UnityEngine.Random.Range(-45f,45f));
                bulletInfo = new BulletInfo(direction, 1, Tower.TowerStat, Tower.transform.position, defaultAttackImage, 0,
                    Tower.TowerStat.FinalAttack);
            } else {
                var startLocation = Tower.transform.position;
                var targetLocation = MathHelper.ExpectedLocation(startLocation, bulletSpeed * RoundManager.Inst.playSpeed,
                    enemy.transform.position,
                    enemy.GetSpeed);

                var direction = (targetLocation - startLocation).normalized;


                bulletInfo = new BulletInfo(direction, 1, Tower.TowerStat, startLocation, defaultAttackImage, 0,
                    Tower.TowerStat.FinalAttack);
            }
            //Debug.Log($"({Tower.TowerStat.BaseAttack} + {Tower.TowerStat.AdditionalAttack}) * (1 + {Tower.TowerStat.AdditionalAttackPercent} / 100f) * {Tower.TowerStat.AdditionalAttackMultiplier}");
            var bulletInfos = Tower.TowerStat.ProcessAttackInfo(bulletInfo);

            foreach (var i in bulletInfos) Tower.StartCoroutine(ShootBullet(i));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Bullet>(info);
            }
        }    
    }
}
