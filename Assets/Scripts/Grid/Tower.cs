using UnityEngine;
using System.Collections.Generic;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        public GridCell Pair { get; private set; }
        public XY Coordinate => Pair.Coordinate;

        public TowerStat TowerStat { get; private set; }

        private AttackBehaviour attackBehaviour;

        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            TowerStat = new TowerStat(this);

            // 일단 디폴트로 총알 쏘도록, 다른거 구현되면 이것도 빼야함 (국사무쌍 같은거)
            attackBehaviour = new BulletAttackBehaviour();
        }

        public void Update()
        {
            attackBehaviour.OnUpdate();
        }

        // TODO: SHOULD BE REMOVED WHEN Init() IS AVAILABLE IN TEST!!!
        public void TempInit()
        {
            TowerStat = new TowerStat(this);
        }

        public Dictionary<string, Sprite> spriteDic = new();
        private Dictionary<string, int> mentsuSpriteOrder = new()
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

            switch(TowerStat.TowerInfo)
            {
                case SingleHaiInfo:
                    //배경 패 하나 깔기
                    var singleHai = TowerStat.TowerInfo.Hais[0];
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
    }
}