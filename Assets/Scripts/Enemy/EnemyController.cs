using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyInfo initEnemyInfo;
        private EnemyStatusEffectList statusEffectList;

        private float _health;
        public float health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    DestroyEnemy();
                }
            }
        }

        public RoundManager RoundManager;
        public void InitEnemy(EnemyInfo paramInfo)
        {
            initEnemyInfo = paramInfo;
            health = initEnemyInfo.initialHealth;
            statusEffectList = new EnemyStatusEffectList();
        }

        public void DestroyEnemy()
        {
            //�ı��ϰ� ����ǰ Ȯ��
            RoundManager.OnEnemyDestroy(gameObject);
        }

        public void MoveForward()
        {
            transform.position -= new Vector3(0, initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f, 0);
        }

        public void OnHit(TowerStat towerStat)
        {
                
        }

        private void Update()
        {
            MoveForward();
            statusEffectList.UpdateListTime();
        }
    }
}
