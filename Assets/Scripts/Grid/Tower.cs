using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MRD
{
    public class Tower : MonoBehaviour {

        private CircleCollider2D EnemyDetector;
        private List<GameObject> EnemyInRange = new List<GameObject>();
        private bool onCoolDown;
        void Start()
        {
            StartCoroutine("ShootBullet", 1);
        }

        public TowerInfo TowerInfo { get; private set; }
        public GridCell Pair { get; private set; }

        public RoundManager RoundManager;

        public GameObject bullet;
        public XY Coordinate => Pair.Coordinate;

        /// <summary>
        /// 모든 타워가 공통으로 가지는 기본 공격력
        /// </summary>
        public float BaseAttack => TowerInfo.Hais.Count * 10;

        /// <summary>
        /// 모든 타워가 공통으로 가지는 기본 공격 속도 (1초에 이만큼 때림)
        /// </summary>
        public float BaseAttackSpeed => 1f;

        public float BaseCritChance => 0.05f;

        public float BaseCritMultiplier => 2;

        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            EnemyDetector = this.GetComponent<CircleCollider2D>();
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
            GameObject TowerImage = new GameObject("TowerImage");
            TowerImage.transform.parent = transform;
            TowerImage.transform.localPosition = Vector3.zero;
            TowerImage.AddComponent<SpriteRenderer>();
            SpriteRenderer TowerImageSpriteRender = TowerImage.GetComponent<SpriteRenderer>();
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

        void FixedUpdate()
        {
            if(!onCoolDown && EnemyInRange.Count > 0) {
                onCoolDown = true;

                Vector3 pos = this.transform.position;
                float minDistance = (pos - EnemyInRange[0].transform.position).sqrMagnitude;
                GameObject proxTeki = EnemyInRange[0];

                for(int i=1;i<EnemyInRange.Count;i++) {
                    var sqrMag = (pos - EnemyInRange[i].transform.position).sqrMagnitude;
                    if((pos - EnemyInRange[i].transform.position).sqrMagnitude < minDistance){
                        minDistance = sqrMag;
                        proxTeki = EnemyInRange[i];
                    }
                }

                ShootBullet(proxTeki);
                StartCoroutine("EnableShooting", 1);
            }
        }
        IEnumerator EnableShooting()
        {
            yield return new WaitForSeconds(1 / (BaseAttackSpeed * RoundManager.playSpeed));
            onCoolDown = false;
        }

        void OnTriggerEnter2D(Collider2D col) // Enemy gets in to attack range, add to watchlist
        {
            if(col.gameObject.tag == "Enemy") 
                if(!EnemyInRange.Contains(col.gameObject)) // Probably unneccesary, but just in case
                    EnemyInRange.Add(col.gameObject);
        }
        void OnTriggerExit2D(Collider2D col) // Enemy goes out of attack range, remove from watchlist
        {
            for(int i=0;i<EnemyInRange.Count;i++)
                if(col.gameObject == EnemyInRange[i])
                    EnemyInRange.RemoveAt(i);
        }

        void ShootBullet(GameObject enemy)
        {
            //총알 생성
            var newBullet = Instantiate(bullet,this.gameObject.transform.position,Quaternion.identity);
            newBullet.GetComponent<Bullet>().setDirection(this.transform.position,enemy,4f);
        }
    }
}