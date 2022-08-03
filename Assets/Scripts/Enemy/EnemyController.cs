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
        private Transform enemyTransform;

        private float health;
        private EnemyInfo initEnemyInfo;
        public float MaxHealth { get; private set; }
        private float bossMaxDamage;
        private EnemyStatusEffectList statusEffectList;
        private BossType bossType = BossType.Nomal;//0:일반몹, 1:강인함, 2:방탄판, 3:광폭화
        private int hitCount = 0;
        private bool inviStat = false;
        
        [System.Flags]
        public enum BossType
        {
            Nomal = 0,
            Tough = 1<<0,
            Invi = 1<<1,
            Berserk = 1<<2,
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
                else if (MaxHealth / 4 >= health)
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
                            if(bossType.HasFlag(BossType.Berserk))
                            {
                                initEnemyInfo.BerserkMod();
                            }
                            break;
                    }
                }
                else if (MaxHealth / 2 >= health)
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

        public Vector3 GetSpeed => DEBUG_MODE
            ? new Vector3(0, -0.5f, 0)
            : new Vector3(0,
                -(initEnemyInfo.initialSpeed * (1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f)) *
                RoundManager.Inst.playSpeed, 0);

        private void Start()
        {
            enemySprite = GetComponent<SpriteRenderer>();
            enemyTransform = transform;
            enemySpriteArr = ResourceDictionary.GetAll<Sprite>("EnemySprite/enemy");
            switch ((int)initEnemyInfo.enemyType)
            {
                case 100:
                    enemySprite.sprite = enemySpriteArr[0];
                    break;
                case 500:
                    enemySprite.sprite = enemySpriteArr[1];
                    enemyTransform.localScale *= 1.2f;
                    break;
                case 1000:
                    enemySprite.sprite = enemySpriteArr[2];
                    enemyTransform.localScale *= 1.4f;
                    break;
                case 5000:
                    enemySprite.sprite = enemySpriteArr[3];
                    enemyTransform.localScale *= 1.7f;
                    break;
                case 10000:
                    enemySprite.sprite = enemySpriteArr[4];
                    enemyTransform.localScale *= 2f;
                    break;
            }
            
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

            endLine = 2f + (RoundManager.Inst.Grid.gridRowLimit - 1) * 0.4f;
            if (endLine + enemyTransform.localScale.x / 2 >= enemyTransform.position.y)
            {
                DestroyEnemy();
                RoundManager.Inst.PlayerDamage((int)initEnemyInfo.enemyType);
            }
        }

        public void InitEnemy(EnemyInfo paramInfo)
        {
            initEnemyInfo = paramInfo;
            Health = initEnemyInfo.initialHealth;
            MaxHealth = initEnemyInfo.initialHealth;
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
                new Vector3(0, initEnemyInfo.initialSpeed * (1 - statusEffectList[EnemyStatusEffectType.PinSlow] * 0.2f),
                    0) * Time.deltaTime * RoundManager.Inst.playSpeed;
        }

        public void OnHit(AttackInfo attackInfo)
        {
            foreach (var i in attackInfo.OnHitOptions) i.OnHit(this);
            float targetDamage = 0f;
            bool isCritical = attackInfo.ShooterTowerStat.FinalCritChance > Random.Range(0f, 1f);

            if (attackInfo is BulletInfo bulletInfo)
                targetDamage = bulletInfo.Damage;
            else if (attackInfo is BladeInfo bladeInfo)
                targetDamage = bladeInfo.ShooterTowerStat.FinalAttack * bladeInfo.DamageMultiplier;
            else if (attackInfo is ExplosiveInfo explosiveInfo)
                targetDamage = explosiveInfo.ShooterTowerStat.FinalAttack * explosiveInfo.DamageMultiplier;
            

            targetDamage *= isCritical ? attackInfo.ShooterTowerStat.FinalCritMultiplier : 1f;

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
            Health -= targetDamage;
            attackInfo.ShooterTowerStat.TowerInfo.TotalDamage += targetDamage;
        }

        public void OnHit(AttackInfo attackInfo, out bool critical)
        {
            foreach (var i in attackInfo.OnHitOptions) i.OnHit(this);
            float targetDamage = 0f;
            bool isCritical = attackInfo.ShooterTowerStat.FinalCritChance > Random.Range(0f, 1f);
            critical = isCritical;

            if (attackInfo is BulletInfo bulletInfo)
                targetDamage = bulletInfo.Damage;
            else if (attackInfo is BladeInfo bladeInfo)
                targetDamage = bladeInfo.ShooterTowerStat.FinalAttack * 1.5f;
            else if (attackInfo is ExplosiveInfo explosiveInfo)
                targetDamage = explosiveInfo.ShooterTowerStat.FinalAttack * 0.5f;

            targetDamage *= isCritical ? attackInfo.ShooterTowerStat.FinalCritMultiplier : 1f;

            Health -= targetDamage;
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
    }
}
