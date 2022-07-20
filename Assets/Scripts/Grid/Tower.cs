using UnityEngine;
using System.Collections.Generic;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        public GridCell Pair { get; private set; }
        public XY Coordinate => Pair.Coordinate;

        public TowerStat TowerStat { get; private set; }

        [SerializeField]
        private Transform ImageParent;

        private AttackBehaviour attackBehaviour;

        public void Init(GridCell gridCellInstance, XY coord, TowerInfo info)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            TowerStat = new TowerStat(info);

            // 일단 디폴트로 총알 쏘도록, 다른거 구현되면 이것도 빼야함 (국사무쌍 같은거)
            attackBehaviour = new BulletAttackBehaviour(AttackImage.Default);
            attackBehaviour.Init(this);
        }

        public void Update()
        {
            attackBehaviour.OnUpdate();
        }

        // TODO: SHOULD BE REMOVED WHEN Init() IS AVAILABLE IN TEST!!!
        public void TempInit()
        {
            TowerStat = new TowerStat(null);
            
            attackBehaviour = new BulletAttackBehaviour(AttackImage.Default);
            attackBehaviour.Init(this);
        }

        private Dictionary<string, Sprite> singleMentsuSpriteDict = new();
        private Dictionary<string, int> mentsuSpriteOrder = new()
        {
            { "BackgroundHai" , 4 },
            { "Wan", 9 },
            { "Sou", 9 },
            { "Pin", 9 },
            { "Kaze", 4 }, 
            { "Sangen", 3 },
            { "Mentsu", 7}
        };

        private Sprite[] tripleSpriteList;

        public void LoadSprites()
        {
            var singleAllSprites = ResourceDictionary.GetAll<Sprite>("TowerSprite/single_mentsu");

            int i = 0;
            foreach (KeyValuePair<string, int> item in mentsuSpriteOrder)
            {
                for (int j = 1; j <= item.Value; j++)
                {
                    singleMentsuSpriteDict.Add(item.Key + j, singleAllSprites[i]);
                    i++;
                }
            }

            tripleSpriteList = ResourceDictionary.GetAll<Sprite>("TowerSprite/triple_tower");
        }

        private SpriteRenderer[] SettingLayer(int n)
        {
            SpriteRenderer[] spriteRenderers = new SpriteRenderer[n];

            int childNum = ImageParent.childCount;

            int layerNum = ImageParent.parent.GetComponent<SpriteRenderer>().sortingOrder;

            Transform backGround = ImageParent.GetChild(0);

            for (int i = childNum; i < n; i++)
            {
                Instantiate(backGround, ImageParent);
            }

            int newChildNum = ImageParent.childCount;

            Transform tmp;
            SpriteRenderer tmpSpriteRenderer;
            for (int i = 0; i < n; i++)
            {
                tmp = ImageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                tmpSpriteRenderer = tmp.GetComponent<SpriteRenderer>();
                tmpSpriteRenderer.sortingOrder = layerNum + i;
                spriteRenderers[i] = tmpSpriteRenderer;
            }

            for (int i = n; i < newChildNum; i++)
            {
                ImageParent.GetChild(i).gameObject.SetActive(false);
            }

            return spriteRenderers;
        }

        public void ApplyTowerImage()
        {
            var towerInfo = TowerStat.TowerInfo;
            int count = towerInfo.Hais.Count;
            
            HaiType type = 0;
            int number = 0;

            if (towerInfo is SingleHaiInfo or MentsuInfo)
            {
                type = towerInfo.Hais[0].Spec.HaiType;

                number = type is HaiType.Kaze or HaiType.Sangen ?
                    towerInfo.Hais[0].Spec.Number + 1 :
                    towerInfo.Hais[0].Spec.Number;


                SpriteRenderer[] spriteRenderers;
                spriteRenderers = count switch
                {
                    1 => SettingLayer(2),
                    _ => SettingLayer(3)        //2, 3, 4
                };

                spriteRenderers[0].sprite = singleMentsuSpriteDict[$"BackgroundHai{count}"];
                spriteRenderers[1].sprite = singleMentsuSpriteDict[type.ToString() + number.ToString()];

                if (count > 1)
                {
                    spriteRenderers[1].transform.localPosition = new Vector2(-0.0315f * (count - 1), 0);

                    spriteRenderers[2].sprite = towerInfo switch
                    {
                        KoutsuInfo koutsu => singleMentsuSpriteDict[$"Mentsu{(koutsu.IsMenzen ? 7 : 6)}"],
                        ShuntsuInfo shuntsu => singleMentsuSpriteDict[$"Mentsu{(shuntsu.IsMenzen ? 5 : 4)}"],
                        KantsuInfo kantsu => singleMentsuSpriteDict[$"Mentsu{(kantsu.IsMenzen ? 3 : 2)}"],
                        _ => singleMentsuSpriteDict["Mentsu1"],
                    };
                }

                /// <summary>
                /// 128 pixel, 카드 하나 늘어날 때마다 4 픽셀 왼쪽 이동
                /// 또이츠면 4 픽셀, 커쯔면 8 픽셀, 깡쯔면 12 픽셀 왼쪽
                /// 작대기 : 또이츠, 네모 : 깡쯔 , 세모 : 슌쯔, 동그라미 : 커쯔
                /// 가운데 막힌게 멘젠
                /// </summary>
            }
            else if (towerInfo is TripleTowerInfo)
            { 
                //TowerOption 중에서 TowerImageOption만 받아오기
                var towerOptions = TowerStat.Options;

                List<TowerImageOption> towerImageOptions = new();

                foreach (var option in towerOptions.Values)
                {
                    if (option.GetType().IsSubclassOf(typeof(TowerImageOption)))
                    {
                        towerImageOptions.Add((TowerImageOption)option);
                    }
                }

                //받아온 TowerImageOption에서 Images 받아오고 Sprite 띄우기
                foreach (var towerImageOption in towerImageOptions)
                {
                    var images = towerImageOption.Images;

                    var spriteRenderers = SettingLayer(images.Count);

                    for (int i = 0; i < images.Count; i++)
                    {
                        spriteRenderers[i].sprite = tripleSpriteList[images[i].index];
                        spriteRenderers[i].sortingOrder += images[i].order;
                    }
                }
            }
        }
    }
}