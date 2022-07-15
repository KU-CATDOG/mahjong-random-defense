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
            attackBehaviour = new BulletAttackBehaviour();
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
        }

        [ContextMenu("test")]
        void test()
        {
            var x = new GameObject();
            x.AddComponent<GridCell>();

            //Init(x.GetComponent<GridCell>(), (0, 0), new SingleHaiInfo(new Hai(1, new HaiSpec(HaiType.Kaze, 1))));
            Init(x.GetComponent<GridCell>(), (0, 0), new KantsuInfo(new Hai(1, new HaiSpec(HaiType.Kaze, 1)),
                                                                    new Hai(1, new HaiSpec(HaiType.Kaze, 1)),
                                                                    new Hai(1, new HaiSpec(HaiType.Kaze, 1)),
                                                                    new Hai(1, new HaiSpec(HaiType.Kaze, 1))));
            LoadSprites();
            ApplyTowerImage();
        }


        private Dictionary<string, Sprite> spriteDic = new();
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
        public void LoadSprites()
        {
            var allSprites = ResourceDictionary.GetAll<Sprite>("TowerSprite/single_mentsu");

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

        private SpriteRenderer[] ExpandLayer(int n)
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
                tmp.localPosition = new Vector3(0, 0, -0.1f);
                tmpSpriteRenderer = tmp.GetComponent<SpriteRenderer>();
                tmpSpriteRenderer.sortingOrder = layerNum;
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
            int count = TowerStat.TowerInfo.Hais.Count;

            HaiType type = 0;
            int number = 0;

            if (count <= 4)
            {
                type = TowerStat.TowerInfo.Hais[0].Spec.HaiType;

                number = type is HaiType.Kaze or HaiType.Sangen ?
                    TowerStat.TowerInfo.Hais[0].Spec.Number + 1 :
                TowerStat.TowerInfo.Hais[0].Spec.Number;
            }
            
            SpriteRenderer[] spriteRenderers;
            switch(count)
            {
                case 1:         //단일 타워
                    spriteRenderers = ExpandLayer(2);
                    spriteRenderers[0].sprite = spriteDic["BackgroundHai1"];
                    spriteRenderers[1].sprite = spriteDic[type.ToString() + number.ToString()];
                    break;
                case 2:         //머리 타워
                    spriteRenderers = ExpandLayer(3);
                    spriteRenderers[0].sprite = spriteDic["BackgroundHai2"];
                    spriteRenderers[1].sprite = spriteDic[type.ToString() + number.ToString()];
                    spriteRenderers[1].transform.localPosition = new Vector3(-0.04f, 0, -0.1f);
                    spriteRenderers[2].sprite = spriteDic["Mentsu1"];
                    break;
                case 3:         //몸통 타워
                    spriteRenderers = ExpandLayer(3);
                    spriteRenderers[0].sprite = spriteDic["BackgroundHai3"];
                    spriteRenderers[1].sprite = spriteDic[type.ToString() + number.ToString()];
                    spriteRenderers[1].transform.localPosition = new Vector3(-0.08f, 0, -0.1f);

                    bool menzenCheck = true;
                    for (int i = 0; i < TowerStat.TowerInfo.Hais.Count; i++)
                    {
                        if (TowerStat.TowerInfo.Hais[i].IsFuroHai)
                        {   //No Menzen
                            menzenCheck = false;
                            break;
                        }
                    }
                    switch(TowerStat.TowerInfo)
                    {
                        case KoutsuInfo:
                            if (menzenCheck)
                            {
                                spriteRenderers[2].sprite = spriteDic["Mentsu7"];
                            }
                            else
                            {
                                spriteRenderers[2].sprite = spriteDic["Mentsu6"];
                            }
                            break;
                        case ShuntsuInfo:
                            if (menzenCheck)
                            {
                                spriteRenderers[2].sprite = spriteDic["Mentsu5"];
                            }
                            else
                            {
                                spriteRenderers[2].sprite = spriteDic["Mentsu4"];
                            }
                            break;
                    }
                    break;
                case 4:
                    spriteRenderers = ExpandLayer(3);
                    spriteRenderers[0].sprite = spriteDic["BackgroundHai4"];
                    spriteRenderers[1].sprite = spriteDic[type.ToString() + number.ToString()];
                    spriteRenderers[1].transform.localPosition = new Vector3(-0.12f, 0, -0.1f);

                    menzenCheck = true;
                    for (int i = 0; i < TowerStat.TowerInfo.Hais.Count; i++)
                    {
                        if (TowerStat.TowerInfo.Hais[i].IsFuroHai)
                        {   //No Menzen
                            menzenCheck = false;
                            break;
                        }
                    }
                    if (menzenCheck)
                    {
                        spriteRenderers[2].sprite = spriteDic["Mentsu3"];
                    }
                    else
                    {
                        spriteRenderers[2].sprite = spriteDic["Mentsu2"];
                    }
                    break;
            }

            //128 pixel, 카드 하나 늘어날 때마다 4 픽셀 왼쪽 이동
            //또이츠면 4 픽셀, 커쯔면 8 픽셀 왼쪽, 깡쯔면 12 픽셀 왼쪽
            //작대기 : 또이츠, 네모 : 깡쯔 , 세모 : 슌쯔, 동그라미 : 커쯔
            //가운데 막힌게 멘젠
            
            //중형타워, 완성타워 필요
        }
    }
}