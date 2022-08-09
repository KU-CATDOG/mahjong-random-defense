using System.Collections;
using UnityEngine;

namespace MRD
{
    public class MinitowerAttackBehaviour : AttackBehaviour
    {
        private float timer;
        private float minX = 0.5f, maxX = 8.5f;
        private RoundManager roundManager = RoundManager.Inst;

        public override void OnInit()
        {
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed * RoundManager.Inst.gameSpeedOnOff;

            if (timer < 0.35f / Tower.TowerStat.FinalStat.AttackSpeed) return;
            timer = 0f;
            Attack();
        }

        private void Attack()
        {
            Vector3 location = new(Random.Range(minX, maxX), roundManager.Grid.RedLineY + 1, 0f);
            var bulletInfo = new MinitowerInfo(Tower.TowerStat, location, AttackImage.Minitower);
            Tower.StartCoroutine(ShootBullet(bulletInfo));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Minitower>(info);
            }
        }
    }
}
