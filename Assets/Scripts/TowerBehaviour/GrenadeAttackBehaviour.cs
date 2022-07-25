using System.Collections;
using UnityEngine;

namespace MRD
{
    public class GrenadeAttackBehaviour : AttackBehaviour
    {
        private float timer = 0f;
        private float angle = 180f;

        public override void OnInit()
        {
        }
        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;

            if ( timer < 0.35f / Tower.TowerStat.FinalAttackSpeed) return;
            float targetAngle = Random.Range(-angle,angle);
            timer = 0f;
            Attack(MathHelper.RotateVector(Vector3.up,targetAngle));
        }
        
        private void Attack(Vector3 direction)
        {
            var bulletInfo = new BulletInfo(direction, 1, Tower.TowerStat, Tower.transform.position, AttackImage.Grenade, 0, 0);
            Tower.StartCoroutine(ShootBullet(bulletInfo));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Grenade>(info);
            }
        }
    }
}
