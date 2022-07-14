using UnityEngine;

namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
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

        public Vector3 GetSpeed => new(0f, -0.05f, 0f);

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

        public void MoveForward()
        {
            transform.position -= new Vector3(0, initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f, 0);
        }

        public void OnHit(AttackInfo attackInfo)
        {
            foreach (var i in attackInfo.OnHitOptions)
            {
                i.OnHit(this);
            }
        }

        private void Update()
        {
            MoveForward();
            statusEffectList.UpdateListTime();
        }
    }
}
