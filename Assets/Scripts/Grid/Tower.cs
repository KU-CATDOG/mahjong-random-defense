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

        [SerializeField]
        private Sprite[] singleTowerImages;
        [SerializeField]
        private Sprite[] nodeTowerImages;
        [SerializeField]
        private Sprite[] tripleTowerImages;
        [SerializeField]
        private Sprite[] completeTowerImages;


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
            towerImageSpriteRender.sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder + 1;

            Sprite settingSprite;
            if (TowerInfo is SingleHaiInfo)
            {
                settingSprite = singleTowerImages[0];           //종류에 맞는 Sprite 출력하게끔 수정해야 함(중장패인지, 노두패인지 등)
            }
            else if (TowerInfo is ToitsuInfo or ShuntsuInfo or KoutsuInfo or KantsuInfo)
            {
                settingSprite = nodeTowerImages[0];
            }
            else if (TowerInfo is TripleTowerInfo)
            {
                settingSprite = tripleTowerImages[0];
            }
            else
            {
                settingSprite = completeTowerImages[0];
            }

            towerImageSpriteRender.sprite = settingSprite;
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
                if (sqrMag > 100f /* Tempvalue of tower attack range */ || sqrMag >= minDistance) continue;
                
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
            // Create bullet and set its position
            var newBullet = Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
            newBullet.GetComponent<Bullet>().InitBullet(gameObject.transform.position,enemy,0.2f,TowerStat);
        }
    }
}