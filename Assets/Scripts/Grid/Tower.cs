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

            enemyDetector = GetComponent<CircleCollider2D>();

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
            if (onCoolDown || enemyInRange.Count <= 0) return;

            onCoolDown = true;

            var pos = transform.position;
            float minDistance = (pos - enemyInRange[0].transform.position).sqrMagnitude;
            var proxTeki = enemyInRange[0];

            for (int i = 1; i < enemyInRange.Count; i++)
            {
                var sqrMag = (pos - enemyInRange[i].transform.position).sqrMagnitude;
                if ((pos - enemyInRange[i].transform.position).sqrMagnitude >= minDistance) continue;

                minDistance = sqrMag;
                proxTeki = enemyInRange[i];
            }

            ShootBullet(proxTeki);
            StartCoroutine(EnableShooting());
        }


        private IEnumerator EnableShooting()
        {
            yield return new WaitForSeconds(1 / (TowerStat.FinalAttackSpeed * roundManager.playSpeed));
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
            //총알 생성
            var newBullet = Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
            newBullet.GetComponent<Bullet>().InitBullet(transform.position,enemy,TowerStat);
        }
    }
}