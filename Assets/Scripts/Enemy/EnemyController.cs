using UnityEngine;

namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        private EnemyInfo initEnemyInfo;
        private EnemyStatusEffectList statusEffectList;

        private float health;

        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    DestroyEnemy();
                }
            }
        }

        public Vector3 GetSpeed => DEBUG_MODE? new Vector3(0,-0.5f,0) : new Vector3(0, initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f, 0);

        public void InitEnemy(EnemyInfo paramInfo)
        {
            initEnemyInfo = paramInfo;
            Health = initEnemyInfo.initialHealth;
            statusEffectList = new EnemyStatusEffectList();
        }

        public void DestroyEnemy()
        {
            // 적이 제거될 때 지급되는 보상 등
            RoundManager.Inst.OnEnemyDestroy(this);
        }

        private void TestMoveForward()
        {
            transform.position += new Vector3(0,-0.5f,0) * Time.deltaTime;
        }
        private void MoveForward()
        {
            transform.position -= new Vector3(0, initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f, 0) * Time.deltaTime;
        }

        public void OnHit(AttackInfo attackInfo)
        {
            foreach (var i in attackInfo.OnHitOptions)
            {
                i.OnHit(this);
            }
        }

        public void GainStatusEffect(EnemyStatusEffectType type, int level) => statusEffectList.GainStatusEffect(type, level);

        private void Update()
        {
            if (DEBUG_MODE)
                TestMoveForward();
            else {
                MoveForward();
                statusEffectList.UpdateListTime();
            }
        }
    }
}
