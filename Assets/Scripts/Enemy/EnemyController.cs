using UnityEngine;
using System.Collections;

namespace MRD
{
    public class EnemyController : MonoBehaviour
    {
        public bool DEBUG_MODE;
        private float endLine;
        private SpriteRenderer enemySprite;
        private Sprite[] enemySpriteArr;
        private Sprite[] enemyEffectSpriteArr;
        private Transform enemyTransform;

        private float health;
        private EnemyInfo initEnemyInfo;
        public float MaxHealth { get; private set; }
        private float bossMaxDamage;
        private EnemyStatusEffectList statusEffectList;
        public BossType bossType = BossType.Normal;
        private int hitCount = 0;
        private bool inviStat = false;
        private int nowPin=0, nowWan=0;
        private GameObject WanState, PinState;
        private SpriteRenderer PinEffectSprite;
        private bool isBoss => MaxHealth >= 10000;

        [System.Flags]
        public enum BossType
        {
            Normal = 0,
            Tough = 1<<0,
            Invi = 1<<1,
            Berserk = 1<<2,
            Split = 1<<3,
            Catapult = 1<<4
        };
        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    if (Random.Range(1, 101) % 10 == 0)
                        RoundManager.Inst.PlusTsumoToken(statusEffectList[EnemyStatusEffectType.WanLoot]);
                    DestroyEnemy();

                }
                else
                {
                    switch ((int)initEnemyInfo.enemyType)
                    {
                        case 100:
                            enemySprite.sprite = enemySpriteArr[2-(int)(health/(MaxHealth / 3))];
                            break;
                        case 500:
                            enemySprite.sprite = enemySpriteArr[3+ 2 - (int)(health / (MaxHealth / 3))];
                            break;
                        case 1000:
                            enemySprite.sprite = enemySpriteArr[6 + 3 - (int)(health / (MaxHealth / 4))];
                            break;
                        case 5000:
                            enemySprite.sprite = enemySpriteArr[10 + 3 - (int)(health / (MaxHealth / 4))];
                            break;
                        case 10000:
                            enemySprite.sprite = enemySpriteArr[14 + 4 - (int)(health / (MaxHealth / 5))];
                            break;
                    }
                    
                }
             
                if (MaxHealth / 4 >= health)
                {
                    if ((int)initEnemyInfo.enemyType == 10000)
                    {
                        if (bossType.HasFlag(BossType.Berserk))
                        {
                            initEnemyInfo.BerserkMod();
                        }
                    }
                }
            }
        }
        public Vector3 GetSpeed => DEBUG_MODE
            ? new Vector3(0, -0.5f, 0)
            : new Vector3(0, -(initEnemyInfo.initialSpeed * Mathf.Max((1 - statusEffectList[EnemyStatusEffectType.PinSlow] 
                    * (0.2f + RoundManager.Inst.RelicManager[typeof(GlueRelic)] * 0.05f)), .1f)),
                    0) * RoundManager.Inst.playSpeed;

        private void Start()
        {
            enemySprite = GetComponent<SpriteRenderer>();
            enemyTransform = transform;
            enemySpriteArr = ResourceDictionary.GetAll<Sprite>("EnemySprite/enemy_new");
            enemyEffectSpriteArr = ResourceDictionary.GetAll<Sprite>("EnemySprite/Status_effect");
            WanState = transform.GetChild(0).gameObject;
            PinState = transform.GetChild(1).gameObject;
            PinEffectSprite = PinState.GetComponent<SpriteRenderer>();

            switch ((int)initEnemyInfo.enemyType)
            {
                case 100:
                    enemySprite.sprite = enemySpriteArr[0];
                    break;
                case 500:
                    enemySprite.sprite = enemySpriteArr[3];
                    enemyTransform.localScale *= 1.2f;
                    break;
                case 1000:
                    enemySprite.sprite = enemySpriteArr[6];
                    enemyTransform.localScale *= 1.4f;
                    break;
                case 5000:
                    enemySprite.sprite = enemySpriteArr[10];
                    enemyTransform.localScale *= 1.7f;
                    break;
                case 10000:
                    enemySprite.sprite = enemySpriteArr[14];
                    enemyTransform.localScale *= 2f;
                    break;
            }
            if (bossType.HasFlag(BossType.Catapult))
                StartCoroutine(ThrowEnemy());


        }

        private void Update()
        {
            if (DEBUG_MODE)
            {
                TestMoveForward();
            }
            else
            {
                MoveForward();
                statusEffectList.UpdateListTime();
            }
            CheckWanPin();

            endLine = RoundManager.Inst.Grid.RedLineY;
            if (endLine + enemyTransform.localScale.x / 2 >= enemyTransform.position.y)
            {
                DestroyEnemy();
                RoundManager.Inst.PlayerDamage((int)initEnemyInfo.enemyType);
            }
      //      Debug.Log(statusEffectList[EnemyStatusEffectType.WanLoot]);
            //Debug.Log(statusEffectList[EnemyStatusEffectType.PinSlow]);
        }

        public void InitEnemy(EnemyInfo paramInfo)
        {
            initEnemyInfo = paramInfo;
            MaxHealth = initEnemyInfo.initialHealth;
            health = initEnemyInfo.initialHealth;
            statusEffectList = new EnemyStatusEffectList();
            bossMaxDamage = (MaxHealth / 100) * 3;
        }

        public void DestroyEnemy()
        {
            // 적이 제거될 때 지급되는 보상 등
            RoundManager.Inst.OnEnemyDestroy(this);
        }

        private void TestMoveForward()
        {
            transform.position += new Vector3(0, -0.5f, 0) * Time.deltaTime;
        }

        private void MoveForward()
        {
            transform.position -=
                new Vector3(0, initEnemyInfo.initialSpeed * (1 - statusEffectList[EnemyStatusEffectType.PinSlow] 
                    * (0.2f + RoundManager.Inst.RelicManager[typeof(PenetratingWoundRelic)] * 0.05f)),
                    0) * Time.deltaTime * RoundManager.Inst.playSpeed * RoundManager.Inst.gameSpeedOnOff;
        }

        private void CheckWanPin()
        {
            if (statusEffectList[EnemyStatusEffectType.WanLoot] != nowWan)
            {
                nowWan = statusEffectList[EnemyStatusEffectType.WanLoot];
                if (nowWan == 0)
                    WanState.SetActive(false);
                else
                    WanState.SetActive(true);

            }
            if (statusEffectList[EnemyStatusEffectType.PinSlow] != nowPin)
            {
                nowPin = statusEffectList[EnemyStatusEffectType.PinSlow];
                if (nowPin == 0)
                    PinState.SetActive(false);
                else {
                    PinState.SetActive(true);
                    PinEffectSprite.sprite = enemyEffectSpriteArr[nowPin];
                }
            }
        }

        public void OnHit(AttackInfo attackInfo)
        {
            bool foo;
            OnHit(attackInfo, out foo);
        }

        public void OnHit(AttackInfo attackInfo, out bool critical, bool InstantDeath = false)
        {
            foreach (var i in attackInfo.OnHitOptions) i.OnHit(this);
            float targetDamage = 0f;
            float extraDamage = 0f;
            bool isCritical = attackInfo.ShooterTowerStat.FinalStat.CritChance 
                + RoundManager.Inst.RelicManager[typeof(BrandRelic)] * ((statusEffectList[EnemyStatusEffectType.WanLoot] > 0) ? 0.1 : 0) > Random.Range(0f, 1f);
            critical = isCritical;

            if (attackInfo is BulletInfo bulletInfo){
                targetDamage = Mathf.Min(bulletInfo.Damage,500);
                if(bulletInfo.PenetrateLevel > 0)
                    extraDamage = health * RoundManager.Inst.RelicManager[typeof(PenetratingWoundRelic)] * 0.05f;
            }
            else if (attackInfo is BladeInfo bladeInfo)
                targetDamage = bladeInfo.ShooterTowerStat.FinalStat.Damage * bladeInfo.DamageMultiplier;
            else if (attackInfo is ExplosiveInfo explosiveInfo)
                targetDamage = explosiveInfo.ShooterTowerStat.FinalStat.Damage * explosiveInfo.DamageMultiplier;
            
            targetDamage *= isCritical ? 1f+attackInfo.ShooterTowerStat.FinalStat.CritDamage : 1f;
            

            if(bossType!=0)
            {
                if (bossType.HasFlag(BossType.Tough)&& targetDamage > bossMaxDamage)
                {
                    targetDamage = bossMaxDamage;
                }
                else if(bossType.HasFlag(BossType.Invi) && hitCount >= 10)
                {
                    inviStat = true;
                    StartCoroutine(InviTime());
                }
            }
            hitCount++;
            if (inviStat)
            {
                hitCount = 0;
                targetDamage = 0;
            }

            if(InstantDeath && isCritical && !isBoss) 
                Health = 0f;
            else 
                Health -= (targetDamage + extraDamage);
            attackInfo.ShooterTowerStat.TowerInfo.TotalDamage += targetDamage;
        }

        public void GainStatusEffect(EnemyStatusEffectType type, int level) =>
            statusEffectList.GainStatusEffect(type, level);

        private IEnumerator InviTime()
        {
            float timer = 0;
            while (timer < 1f)
            {
                timer += Time.deltaTime * RoundManager.Inst.playSpeed * RoundManager.Inst.gameSpeedOnOff;
                yield return null;
            }
            inviStat = false;
        }

        private IEnumerator ThrowEnemy()
        {
            GameObject Enemy = RoundManager.Inst.Spawner.Enemy;
            float timer = 0;
            while (true)
            {
                while (timer < 4f)
                {
                    timer += Time.deltaTime * RoundManager.Inst.playSpeed * RoundManager.Inst.gameSpeedOnOff;
                    yield return null;
                }
                var initEnemyInfo = new EnemyInfo(EnemyType.E500, 300, 3.6f);
                var newEnemy = Instantiate(Enemy, this.transform);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.Inst.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
            }
        }
        
    }
}
