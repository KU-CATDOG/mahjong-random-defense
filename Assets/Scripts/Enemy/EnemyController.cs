using UnityEngine;

namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        private EnemyInfo initEnemyInfo;
        private EnemyStatusEffectList statusEffectList;
        private SpriteRenderer enemySprite;
        private Sprite[] enemySpriteArr;

        private float health;
        private float maxHealth;
        private float endLine;

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
                else if(maxHealth / 2 >= health)
                {
                    switch ((int)initEnemyInfo.enemyType)
                    {
                        case 100:
                            enemySprite.sprite = enemySpriteArr[10];
                            break;
                        case 500:
                            enemySprite.sprite = enemySpriteArr[11];
                            break;
                        case 1000:
                            enemySprite.sprite = enemySpriteArr[12];
                            break;
                        case 5000:
                            enemySprite.sprite = enemySpriteArr[13];
                            break;
                        case 10000:
                            enemySprite.sprite = enemySpriteArr[14];
                            break;
                    }
                }
                else if (maxHealth / 4 >= health)
                {
                    switch ((int)initEnemyInfo.enemyType)
                    {
                        case 100:
                            enemySprite.sprite = enemySpriteArr[5];
                            break;
                        case 500:
                            enemySprite.sprite = enemySpriteArr[6];
                            break;
                        case 1000:
                            enemySprite.sprite = enemySpriteArr[7];
                            break;
                        case 5000:
                            enemySprite.sprite = enemySpriteArr[8];
                            break;
                        case 10000:
                            enemySprite.sprite = enemySpriteArr[9];
                            break;
                    }
                }
            }
        }

        private void Start()
        {
            enemySprite = this.GetComponent<SpriteRenderer>();
            enemySpriteArr = ResourceDictionary.GetAll<Sprite>("EnemySprite");
            switch ((int)initEnemyInfo.enemyType)
            {
                case 100:
                    enemySprite.sprite = enemySpriteArr[0];
                    break;
                case 500:
                    enemySprite.sprite = enemySpriteArr[1];
                    break;
                case 1000:
                    enemySprite.sprite = enemySpriteArr[2];
                    break;
                case 5000:
                    enemySprite.sprite = enemySpriteArr[3];
                    break;
                case 10000:
                    enemySprite.sprite = enemySpriteArr[4];
                    break;
            }
        }
        public Vector3 GetSpeed => DEBUG_MODE? new Vector3(0,-0.5f,0) : new Vector3(0, -(initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f) * RoundManager.Inst.playSpeed, 0) ;

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
            transform.position -= new Vector3(0, initEnemyInfo.initialSpeed * 1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f, 0) * Time.deltaTime * RoundManager.Inst.playSpeed;
        }

        public void OnHit(AttackInfo attackInfo)
        {
            if(attackInfo is BulletInfo bulletInfo)
                Health -= bulletInfo.Damage;
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
            endLine = 2f + ((RoundManager.Inst.Grid.gridRowLimit - 1) * 0.4f);
            if ((endLine + 0.5f) >= this.transform.position.y)
            { 
                DestroyEnemy();
                RoundManager.Inst.PlayerDamage((int)initEnemyInfo.enemyType);
            }
        }
    }
}
