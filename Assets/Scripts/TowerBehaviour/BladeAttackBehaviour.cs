using System.Collections;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class BladeAttackBehaviour : AttackBehaviour
    {
        private float angle = 180f;
        private float timer;
        public bool Type;

        public BladeAttackBehaviour(bool Type) => this.Type = Type;

        private (float, int) timerInfo => Type ? (0.05f, 30) : (0.1f, 15);

        public override void OnInit()
        {
        }

        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;

            if (timer < 0.35f / Tower.TowerStat.FinalAttackSpeed) return;
            timer = 0f;
            Attack();
        }

        private void Attack()
        {
            RoundManager.Inst.AttachTimer(timerInfo.Item1, timerInfo.Item2, Tower, ShootBullet);
            //Tower.StartCoroutine(ShootBullet(bladeInfo));

            static IEnumerator ShootBullet(Tower tower)
            {
                var pos = tower.transform.position;
                var enemyList = RoundManager.Inst.Spawner.EnemyList;
                EnemyController targetTeki = null;
                var enemyInRange = enemyList.Where(enemy => (pos - enemy.transform.position).sqrMagnitude <= 144f)
                    .ToList();
                if (enemyInRange.Count <= 0) yield break;

                int index = Random.Range(0, enemyInRange.Count);
                targetTeki = enemyList[index];
                var info = new BladeInfo(targetTeki, targetTeki.transform.position, tower.TowerStat,
                    targetTeki.transform.position, AttackImage.Blade, damageToTarget: true);
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Blade>(info);
            }
        }
    }
}
