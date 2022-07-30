using System.Collections;
using UnityEngine;
using System.Linq;

namespace MRD
{
    public class BladeAttackBehaviour : AttackBehaviour
    {
        public bool Type = false;
        private (float, int) timerInfo => Type? (0.05f, 30) : (0.1f, 15);
        private float timer = 0f;
        private float angle = 180f;
        public BladeAttackBehaviour(bool Type){
            this.Type = Type;
        }
        public override void OnInit()
        {
        }
        public override void OnUpdate()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;

            if ( timer < 0.35f / Tower.TowerStat.FinalAttackSpeed) return;
            float targetAngle = Random.Range(-angle,angle);
            timer = 0f;
            Attack();
        }
        
        private void Attack()
        {
            var pos = Tower.transform.position;
            var enemyList = RoundManager.Inst.Spawner.EnemyList;
            EnemyController targetTeki = null;
            var enemyInRange = enemyList.Where(enemy => (pos - enemy.transform.position).sqrMagnitude <= 144f).ToList();
            if (enemyInRange.Count <= 0) return;

            int index = Random.Range(0, enemyInRange.Count);
            targetTeki = enemyList[index];
            var bladeInfo = new BladeInfo(targetTeki,targetTeki.transform.position,Tower.TowerStat,targetTeki.transform.position,"");

            RoundManager.Inst.AttachTimer(timerInfo.Item1,timerInfo.Item2,Tower,bladeInfo,ShootBullet);
            //Tower.StartCoroutine(ShootBullet(bladeInfo));

            static IEnumerator ShootBullet(AttackInfo info)
            {
                yield return new WaitForSeconds(info.ShootDelay);

                AttackGenerator.GenerateAttack<Blade>(info);
            }
        }
    }
}
