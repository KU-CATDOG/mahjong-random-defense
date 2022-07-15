using System.Collections;
using UnityEngine;

namespace MRD
{
    public class BulletAttackBehaviour : AttackBehaviour
    {
        private float lastShootTime = float.MinValue;

        public override void OnUpdate()
        {
            var now = Time.time;

            if (now - lastShootTime < Tower.TowerStat.FinalAttackSpeed) return;

            var enemyList = RoundManager.Inst.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = Tower.transform.position;
            float minDistance = 1000000000f;
            EnemyController proxTeki = null;

            foreach (var enemy in enemyList)
            {
                var sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag > 1000f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;

                minDistance = sqrMag;
                proxTeki = enemy;
            }

            if(proxTeki == null)
            {
                return;
            }

            lastShootTime = now;
            Attack(proxTeki);
        }

        private void Attack(EnemyController enemy)
        {
            var startLocation = Tower.transform.position;
            var targetLocation = ExpectedLocation(startLocation, 5f, enemy.transform.position,
                enemy.GetSpeed);

            var direction = (targetLocation - startLocation).normalized;

            var bulletInfo = new BulletInfo(direction, 1, Tower.TowerStat, startLocation, "default", 0);
            //bulletInfo.MaxPenetrateCount = 3; //SHOULD BE ENABLED ON TEST

            // STARTRANGE SHOULD BE DISABLED WHEN TESTING!!
            var bulletInfos = Tower.TowerStat.ProcessAttackInfo(bulletInfo);

            foreach (var i in bulletInfos)
            {
                Tower.StartCoroutine(ShootBullet(i));
            }
            // ENDRANGE SHOULD BE DISABLED WHEN TESTING!!

            Tower.StartCoroutine(ShootBullet(bulletInfo));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Bullet>(info);
            }
        }

        public static Vector3 ExpectedLocation(Vector3 bP, float bV, Vector3 eP, Vector3 eV)
        {
            var t = QuadraticEquation(eV.x * eV.x + eV.y * eV.y - bV * bV,
                2 * (eV.x * (eP.x - bP.x) + eV.y * (eP.y - bP.y)),
                (eP.y - bP.y) * (eP.y - bP.y) + (eP.x - bP.x) * (eP.x - bP.x));
            return eP + eV * t;
        }

        // TODO: Fix t<0 when a=0 (probably unnecessary)
        private static float QuadraticEquation(float a, float b, float c)
        {
            if (a == 0) return -c / b;
            return (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        }
    }
}
