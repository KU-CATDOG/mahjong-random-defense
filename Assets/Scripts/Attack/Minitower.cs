using UnityEngine;
using System.Collections;
namespace MRD
{
    public class Minitower : Attack
    {
        public MinitowerInfo MinitowerInfo => (MinitowerInfo)attackInfo;
        private float timer = 0f;
        private float timer1 = 0f;

        [SerializeField]
        private GameObject barrel;
        private float attackSpeed;
        private float baseBulletSpeed = 10f;
        private bool en;
        private float maxAngle = 80f;
        private HaiType shupaiType => MinitowerInfo.TowerStat.TowerInfo.Hais[0].Spec.HaiType;
        private AttackImage attackImage => shupaiType switch {
                HaiType.Pin => AttackImage.Pin,
                HaiType.Sou => AttackImage.Sou,
                HaiType.Wan => AttackImage.Wan,
                _ => AttackImage.Default,
            };

        private void Update()
        {
            OnUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*
            if (!collision.gameObject.CompareTag("Enemy")) return;

            if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

            enemy.OnHit(MinitowerInfo, out bool critical);
            */
        }

        protected virtual void OnUpdate()
        {
            if(!en) return;
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            timer1 += Time.deltaTime * RoundManager.Inst.playSpeed;

            if(timer1 > 10f) {
                en=false;
                Destroy(gameObject);
                return;
            }

            if( timer < 0.35f / attackSpeed) return;

            var enemyList = RoundManager.Inst.Spawner.EnemyList;

            if (enemyList.Count <= 0) return;

            var pos = MinitowerInfo.StartPosition;
            EnemyController targetTeki = null;

            float minDistance = 1000000000f;
            foreach (var enemy in enemyList)
            {
                float sqrMag = (pos - enemy.transform.position).sqrMagnitude;
                if (sqrMag > 144f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;

                float angle = Vector3.Angle(Vector3.up, (enemy.transform.position - pos).normalized);
                if (angle > maxAngle || angle < -maxAngle) continue;

                minDistance = sqrMag;
                targetTeki = enemy;
            }

            if (targetTeki == null)
                return;

            timer = 0f;
            Attack(targetTeki);
        }

        protected override void OnInit()
        {
            attackSpeed = MinitowerInfo.TowerStat.FinalAttackSpeed * 5;
            en = true;
        }

        private void Attack(EnemyController enemy)
        {
            var startLocation = transform.position;
            var targetLocation = MathHelper.ExpectedLocation(startLocation, baseBulletSpeed * RoundManager.Inst.playSpeed,
                enemy.transform.position,
                enemy.GetSpeed);

            var direction = (targetLocation - startLocation).normalized;

            var bulletInfo = new BulletInfo(direction, 1, MinitowerInfo.TowerStat, startLocation, attackImage, 0,
                MinitowerInfo.TowerStat.FinalAttack);

            bulletInfo.UpdateShupaiLevel(shupaiType,4);
            StartCoroutine(ShootBullet(bulletInfo));
            barrel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, MathHelper.GetAngle(Vector3.up, direction) * 2));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Bullet>(info);
            }
        }
    }
}
