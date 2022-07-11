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

        public void InitEnemy(EnemyInfo paramInfo)
        {
            initEnemyInfo = paramInfo;
            health = initEnemyInfo.initialHealth;
            statusEffectList = new EnemyStatusEffectList();
        }

        public void DestroyEnemy()
        {
            //파괴하고 전리품 확인
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
