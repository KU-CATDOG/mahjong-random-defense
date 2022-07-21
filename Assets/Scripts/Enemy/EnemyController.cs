using UnityEngine;

namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
        public bool DEBUG_MODE = false;
        private EnemyInfo initEnemyInfo;
        private EnemyStatusEffectList statusEffectList;
        private SpriteRenderer enemySprite;

        private float health;
        private float maxHealth;
        private float endLine;

        private string spriteString;

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
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/crack100p");
                            break;
                        case 500:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/crack500p");
                            break;
                        case 1000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/crack1000p");
                            break;
                        case 5000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/crack5000p");
                            break;
                        case 10000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/crack10000p");
                            break;
                    }
                }
                else if (maxHealth / 4 >= health)
                {
                    switch ((int)initEnemyInfo.enemyType)
                    {
                        case 100:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/broken100p");
                            break;
                        case 500:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/broken500p");
                            break;
                        case 1000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/broken1000p");
                            break;
                        case 5000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/broken5000p");
                            break;
                        case 10000:
                            enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/broken10000p");
                            break;
                    }
                }
            }
        }

        private void Start()
        {
            enemySprite = this.GetComponent<SpriteRenderer>();
            switch ((int)initEnemyInfo.enemyType)
            {
                case 100:
                    enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/100p");
                    break;
                case 500:
                    enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/500p");
                    break;
                case 1000:
                    enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/1000p");
                    break;
                case 5000:
                    enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/5000p");
                    break;
                case 10000:
                    enemySprite.sprite = Resources.Load<Sprite>("EnemySprite/10000p");
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
            endLine = 2f + ((RoundManager.Inst.Grid.gridRowLimit - 1) * 0.4f);//  endLine= 0.4f + ((타워row갯수-1)*0.4f) 임시
            if ((endLine + 0.5f) >= this.transform.position.y)
            { 
                DestroyEnemy();
                RoundManager.Inst.PlayerDamage((int)initEnemyInfo.enemyType);
            }
        }
    }
}
