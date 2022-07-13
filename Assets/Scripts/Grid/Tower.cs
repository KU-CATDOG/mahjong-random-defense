using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        private CircleCollider2D enemyDetector;
        private List<GameObject> enemyInRange = new List<GameObject>();
        private bool onCoolDown;

        public TowerInfo TowerInfo { get; private set; }
        public GridCell Pair { get; private set; }

        public RoundManager roundManager;

        public GameObject bullet;
        public XY Coordinate => Pair.Coordinate;

        public TowerStat TowerStat { get; private set; }

        public void Start(){
            roundManager = RoundManager.Inst;
        }
        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            TowerStat = new TowerStat(this);
        }

        // TODO: SHOULD BE REMOVED WHEN Init() IS AVAILABLE IN TEST!!!
        public void TempInit()
        {
            TowerStat = new TowerStat(this);
        }


        public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
        private Dictionary<string, int> mentsuSpriteOrder = new Dictionary<string, int>()
        {
            { "BackgroundHai" , 4 },
            { "Sou", 9 },
            { "Wan", 9 },
            { "Pin", 9 }, 
            { "Sangen", 2 },
            { "Kaze", 4 },
            { "Mentsu", 7}
        };

        public void LoadSprites()
        {
            Sprite[] allSprites = Resources.LoadAll<Sprite>("TowerSprite/single_mentsu");

            int i = 0;
            foreach (KeyValuePair<string, int> item in mentsuSpriteOrder)
            {
                for (int j = 1; j <= item.Value; j++)
                {
                    spriteDic.Add(item.Key + j, allSprites[i]);
                    i++;
                }
            }
        }

        //For Test
        public void AddTowerInfo(TowerInfo tower)
        {
            TowerInfo = tower;
        }

        public void ApplyTowerImage()
        {
            var towerImage = new GameObject("TowerImage")
            {
                transform =
                {
                    parent = transform,
                    localPosition = Vector3.zero,
                },
            };

            towerImage.AddComponent<SpriteRenderer>();
            var towerImageSpriteRender = towerImage.GetComponent<SpriteRenderer>();
            towerImageSpriteRender.sortingLayerName = "BattleField";
            towerImageSpriteRender.sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;

            switch(TowerInfo)
            {
                case SingleHaiInfo:
                    //배경 패 하나 깔기
                    var singleHai = TowerInfo.Hais[0];
                    //일치하는 패 깔기

                    break;
                case ToitsuInfo:
                    break;
                case KoutsuInfo:
                    break;
                case KantsuInfo:
                    break;
                case ShuntsuInfo:
                    break;
                case TripleTowerInfo:
                    break;
                case CompleteTowerInfo:
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (onCoolDown || roundManager.EnemyList.Count <= 0) return;

            var enemyList = roundManager.EnemyList;
            onCoolDown = true;

            Vector3 pos = transform.position;
            float minDistance = 1000000000f;
            GameObject proxTeki = null;

            for (int i = 0; i < enemyList.Count; i++)
            {
                var sqrMag = (pos - enemyList[i].transform.position).sqrMagnitude;
                if (sqrMag > 1000f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;
                
                minDistance = sqrMag;
                proxTeki = enemyList[i];
            }
            if(proxTeki == null) {
                onCoolDown = false;
                return;
            }
            ShootBullet(proxTeki);
            StartCoroutine(EnableShooting());
        }
        private IEnumerator EnableShooting()
        {
            yield return new WaitForSeconds(1 /* / (TowerStat.FinalAttackSpeed * roundManager.playSpeed )*/);
            onCoolDown = false;
        }

        private void OnTriggerEnter2D(Collider2D col) // Enemy gets in to attack range, add to watchlist
        {
            if (!col.gameObject.CompareTag("Enemy")) return;

            if(!enemyInRange.Contains(col.gameObject)) // Probably unnecessary, but just in case
                enemyInRange.Add(col.gameObject);
        }

        private void OnTriggerExit2D(Collider2D col) // Enemy goes out of attack range, remove from watchlist
        {
            for (int i = 0; i < enemyInRange.Count; i++)
            {
                if (col.gameObject == enemyInRange[i])
                    enemyInRange.RemoveAt(i);
            }
        }

        private void ShootBullet(GameObject enemy)
        {
            var bulletOptions = TowerStat.OnShootOption;
            var newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity).GetComponent<Bullet>();
            newBullet.InitBullet(gameObject.transform.position, enemy, 0.2f, new BulletInfo(1f, 0f), TowerStat, bulletOptions);
            // Create bullet and set its position
            foreach (var bulletInfo in TowerStat.AdditionalBullet)
            {
                newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity).GetComponent<Bullet>();
                newBullet.InitBullet(gameObject.transform.position, enemy, 0.2f, bulletInfo, TowerStat, bulletOptions);
            }
        }
    } 
}