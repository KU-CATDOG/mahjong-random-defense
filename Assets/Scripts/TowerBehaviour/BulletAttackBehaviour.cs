using System.Collections;
using UnityEngine;

namespace MRD
{
    public class BulletAttackBehaviour : AttackBehaviour
    {
        private AttackImage defaultAttackImage = AttackImage.Default;
        private float timer = 0f;
        public override void OnInit()
        {
            if(Tower.TowerStat.TowerInfo != null)
                defaultAttackImage = Tower.TowerStat.TowerInfo.DefaultAttackImage;
        }
        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;

            if ( timer < Tower.TowerStat.FinalAttackSpeed) return;

            var enemyList = RoundManager.Inst.Spawner.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = Tower.transform.position;
            float minDistance = 1000000000f;
            EnemyController proxTeki = null;

            foreach (var enemy in enemyList)
            {
                var sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag > 144f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;

                minDistance = sqrMag;
                proxTeki = enemy;
            }

            if(proxTeki == null)
                return;
            

            timer = 0f;
            Attack(proxTeki);
        }
        
        private void Attack(EnemyController enemy)
        {
            // FIXME: ExpectedLocation이 bulletinfo를 처리하기 전에 계산되어 일부 option들이 제대로 작동하지 않을 수 있음
            // TODO: BulletInfo.TargetTo에 따른 목표 지정 구현(HighestHp, Random)
            var startLocation = Tower.transform.position;
            var targetLocation = ExpectedLocation(startLocation, 5f * RoundManager.Inst.playSpeed, enemy.transform.position,
                enemy.GetSpeed);

            var direction = (targetLocation - startLocation).normalized;

            var bulletInfo = new BulletInfo(direction, 1, Tower.TowerStat, startLocation, defaultAttackImage, 0);
            
            var bulletInfos = Tower.TowerStat.ProcessAttackInfo(bulletInfo);

            foreach (var i in bulletInfos)
            {
                Tower.StartCoroutine(ShootBullet(i));
            }

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
