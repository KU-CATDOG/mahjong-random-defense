using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        private CircleCollider2D EnemyDetector;
        private List<GameObject> EnemyInRange = new List<GameObject>();
        private bool onCoolDown;

        private void Start()
        {
            StartCoroutine("ShootBullet", 1);
        }

        public TowerInfo TowerInfo { get; private set; }
        public GridCell Pair { get; private set; }

        public RoundManager RoundManager;

        public GameObject bullet;
        public XY Coordinate => Pair.Coordinate;

        public TowerStat TowerStat { get; private set; }

        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            EnemyDetector = GetComponent<CircleCollider2D>();

            TowerStat = new TowerStat(this);
        }

        [SerializeField]
        private Sprite[] SingleTowerImages;
        [SerializeField]
        private Sprite[] NodeTowerImages;
        [SerializeField]
        private Sprite[] TripleTowerImages;
        [SerializeField]
        private Sprite[] CompleteTowerImages;


        public void ApplyTowerImage()
        {
            var TowerImage = new GameObject("TowerImage");
            TowerImage.transform.parent = transform;
            TowerImage.transform.localPosition = Vector3.zero;
            TowerImage.AddComponent<SpriteRenderer>();
            var TowerImageSpriteRender = TowerImage.GetComponent<SpriteRenderer>();
            TowerImageSpriteRender.sortingLayerName = "BattleField";
            TowerImageSpriteRender.sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder + 1;

            Sprite SettingSprite;
            if (TowerInfo is SingleHaiInfo)
            {
                SettingSprite = SingleTowerImages[0];           //종류에 맞는 Sprite 출력하게끔 수정해야 함(중장패인지, 노두패인지 등)
            }
            else if (TowerInfo is ToitsuInfo or ShuntsuInfo or KoutsuInfo or KantsuInfo)
            {
                SettingSprite = NodeTowerImages[0];
            }
            else if (TowerInfo is TripleTowerInfo)
            {
                SettingSprite = TripleTowerImages[0];
            }
            else
            {
                SettingSprite = CompleteTowerImages[0];
            }

            TowerImageSpriteRender.sprite = SettingSprite;
        }

        private void FixedUpdate()
        {
            if (onCoolDown || EnemyInRange.Count <= 0) return;

            onCoolDown = true;

            var pos = transform.position;
            float minDistance = (pos - EnemyInRange[0].transform.position).sqrMagnitude;
            var proxTeki = EnemyInRange[0];

            for (int i = 1; i < EnemyInRange.Count; i++)
            {
                var sqrMag = (pos - EnemyInRange[i].transform.position).sqrMagnitude;
                if ((pos - EnemyInRange[i].transform.position).sqrMagnitude >= minDistance) continue;

                minDistance = sqrMag;
                proxTeki = EnemyInRange[i];
            }

            ShootBullet(proxTeki);
            StartCoroutine(EnableShooting());
        }


        private IEnumerator EnableShooting()
        {
            yield return new WaitForSeconds(1 / (TowerStat.FinalAttackSpeed * RoundManager.playSpeed));
            onCoolDown = false;
        }

        private void OnTriggerEnter2D(Collider2D col) // Enemy gets in to attack range, add to watchlist
        {
            if (!col.gameObject.CompareTag("Enemy")) return;

            if(!EnemyInRange.Contains(col.gameObject)) // Probably unneccesary, but just in case
                EnemyInRange.Add(col.gameObject);
        }

        private void OnTriggerExit2D(Collider2D col) // Enemy goes out of attack range, remove from watchlist
        {
            for (int i = 0; i < EnemyInRange.Count; i++)
            {
                if (col.gameObject == EnemyInRange[i])
                    EnemyInRange.RemoveAt(i);
            }
        }

        private void ShootBullet(GameObject enemy)
        {
            //총알 생성
            var newBullet = Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
            newBullet.GetComponent<Bullet>().setDirection(transform.position,enemy,4f);
        }
    }
}