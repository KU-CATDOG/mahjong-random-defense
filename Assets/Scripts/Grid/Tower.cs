using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        private static readonly Dictionary<string, Sprite> singleMentsuSpriteDict = new();

        private static readonly Dictionary<string, int> mentsuSpriteOrder = new()
        {
            { "BackgroundHai", 4 },
            { "Wan", 9 },
            { "Sou", 9 },
            { "Pin", 9 },
            { "Kaze", 4 },
            { "Sangen", 3 },
            { "Mentsu", 7 },
            { "FuroHai", 1 },
        };

        public static Sprite[] TripleSpriteList;
        public static Sprite[] CompleteSpriteList;

        [SerializeField]
        private Transform ImageParent;

        private AttackBehaviour attackBehaviour;
        public GridCell Pair { get; private set; }
        public XY Coordinate => Pair.Coordinate;

        public TowerStat TowerStat { get; private set; }
        public static IReadOnlyDictionary<string, Sprite> SingleMentsuSpriteDict => singleMentsuSpriteDict;

        public void Update()
        {
            if (TowerStat.TowerInfo != null)
                attackBehaviour.OnUpdate();
        }

        public void Init(GridCell gridCellInstance, XY coord, TowerInfo info)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);

            TowerStat = new TowerStat(this, info);
            TowerStat.UpdateStat();

            attackBehaviour = TowerStat.AttackBehaviour;
            attackBehaviour.Init(this);
        }

        public void SetTower(TowerInfo info)
        {
            TowerStat = new TowerStat(this, info);
            TowerStat.UpdateOptions();

            attackBehaviour = TowerStat.AttackBehaviour;
            attackBehaviour.Init(this);

            // Remove RiChi animation of previous tower if not ended properly
            var imp = Pair.ImageParent;
            if (imp.childCount > 0)
            {
                var animator = imp.GetChild(imp.childCount-1).GetComponent<RiChiAnimator>();
                if (animator != null)
                    Destroy(animator);
            }
        }

        // TODO: SHOULD BE REMOVED WHEN Init() IS AVAILABLE IN TEST!!!
        public void TempInit()
        {
            TowerStat = new TowerStat(this, null);

            attackBehaviour = new BulletAttackBehaviour();
            attackBehaviour.Init(this);
        }

        public static void LoadSprites()
        {
            var singleAllSprites = ResourceDictionary.GetAll<Sprite>("TowerSprite/single_mentsu");

            int i = 0;
            foreach (var item in mentsuSpriteOrder)
            {
                for (int j = 1; j <= item.Value; j++)
                {
                    singleMentsuSpriteDict.Add(item.Key + j, singleAllSprites[i]);
                    i++;
                }
            }

            TripleSpriteList = ResourceDictionary.GetAll<Sprite>("TowerSprite/triple_tower");
            CompleteSpriteList = ResourceDictionary.GetAll<Sprite>("TowerSprite/complete_tower");
        }

        private SpriteRenderer[] SetLayers(int n)
        {
            var spriteRenderers = new SpriteRenderer[n];

            int childNum = ImageParent.childCount;

            int layerNum = GetComponent<SpriteRenderer>().sortingOrder;

            var backGround = ImageParent.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(backGround, ImageParent);

            int newChildNum = ImageParent.childCount;

            Transform tmp;
            SpriteRenderer tmpSpriteRenderer;
            for (int i = 0; i < n; i++)
            {
                tmp = ImageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                tmp.localPosition = Vector3.zero;
                tmpSpriteRenderer = tmp.GetComponent<SpriteRenderer>();
                tmpSpriteRenderer.sortingOrder = layerNum + 1;
                spriteRenderers[i] = tmpSpriteRenderer;
            }

            for (int i = n; i < newChildNum; i++) ImageParent.GetChild(i).gameObject.SetActive(false);

            return spriteRenderers;
        }

        public void ApplyTowerImage()
        {
            var towerInfo = TowerStat.TowerInfo;
            if (towerInfo == null)
            {
                SetLayers(0);
                return;
            }

            int count = towerInfo.Hais.Count;

            if (towerInfo is SingleHaiInfo or MentsuInfo)
            {
                // attack tower
                var type = towerInfo.Hais[0].Spec.HaiType;

                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? towerInfo.Hais[0].Spec.Number + 1
                    : towerInfo.Hais[0].Spec.Number;


                SpriteRenderer[] spriteRenderers;
                spriteRenderers = count switch
                {
                    1 => SetLayers(2),
                    _ => SetLayers(3), //2, 3, 4
                };

                spriteRenderers[0].sprite = singleMentsuSpriteDict[$"BackgroundHai{count}"];
                spriteRenderers[1].sprite = singleMentsuSpriteDict[type + number.ToString()];

                if (count > 1)
                    spriteRenderers[2].sprite = towerInfo switch
                    {
                        KoutsuInfo koutsu => singleMentsuSpriteDict[$"Mentsu{(koutsu.IsMenzen ? 7 : 6)}"],
                        ShuntsuInfo shuntsu => singleMentsuSpriteDict[$"Mentsu{(shuntsu.IsMenzen ? 5 : 4)}"],
                        KantsuInfo kantsu => singleMentsuSpriteDict[$"Mentsu{(kantsu.IsMenzen ? 3 : 2)}"],
                        _ => singleMentsuSpriteDict["Mentsu1"],
                    };

                for (int i = 0; i < spriteRenderers.Length; i++) spriteRenderers[i].sortingOrder += i;

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
                    if (option is TowerImageOption image)
                        towerImageOptions.Add(image);

                //받아온 TowerImageOption에서 Images 받아와서 imageList에 저장
                List<(int index, int order)> imagesList = new();

                foreach (var towerImageOption in towerImageOptions)
                {
                    var images = towerImageOption.Images;

                    foreach ((int i, int o) in images) imagesList.Add((i, o));
                }

                //imageList 이용해 이미지 출력
                int layerCount = 1;
                var spriteRenderers = SetLayers(imagesList.Count + 1);
                spriteRenderers[0].sprite = TripleSpriteList[0];

                foreach ((int index, int order) in imagesList)
                {
                    spriteRenderers[layerCount].sprite = TripleSpriteList[index];
                    spriteRenderers[layerCount].sortingOrder += order;
                    layerCount++;
                }
            }
            else if (towerInfo is YakuHolderInfo holder)
            {
                var imagesList = TowerStat.Options.Values
                    .Where(x => x is TowerImageOption)
                    .Cast<TowerImageOption>()
                    .SelectMany(x => x.Images)
                    .ToList();

                int layerCount = 1;
                var spriteRenderers = SetLayers(imagesList.Count + 1);
                spriteRenderers[0].sprite = CompleteSpriteList[holder.YakuList.Count == 0 || !holder.YakuList[0].IsYakuman ? 0 : 33];

                foreach ((int index, int order) in imagesList)
                {
                    spriteRenderers[layerCount].sprite = CompleteSpriteList[index];
                    spriteRenderers[layerCount].sortingOrder += order;
                    layerCount++;
                }
            }
        }
    }
}
