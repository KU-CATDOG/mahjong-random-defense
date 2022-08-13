using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class TowerStatImageController : MonoBehaviour
    {
        [SerializeField]
        private GameObject yakusBackGround;

        [SerializeField]
        private GameObject backGround;

        [SerializeField]
        private Transform imageParent;

        [SerializeField]
        private GameObject textParent;
        [SerializeField]
        private ClickUI richiButton;
        private Sprite[] richiButtonSprite;
        private Image richiButtonImage;

        [SerializeField]
        private GameObject ClickStatButton;

        [SerializeField]
        private float startingMentsuPos;

        [SerializeField]
        private float mentsuGap;

        [SerializeField]
        private float towerGap;

        [SerializeField]
        private float haiFuroGap;

        [SerializeField]
        private float compressionRatio;

        public Text attackText;
        public Text attackSpeedText;
        public Text criticalChanceText;
        public Text criticalMutiplierText;
        public Text damageAmountText;

        public Text yakusText;

        private TowerInfo towerInfo;

        private TowerStat towerStat;
        public bool isYakuTextEnabled { get; private set; }

        private void Start()
        {
            richiButtonSprite = ResourceDictionary.GetAll<Sprite>("UISprite/richi_button");
            richiButtonImage = richiButton.GetComponent<Image>();
            richiButton.AddListenerOnly(() => {
                if(towerInfo != null && towerInfo.RichiInfo is RichiInfo richiInfo && richiInfo.State == RichiState.Ready)
                    towerInfo.RichiInfo.EnableRichi();
            });
        }
        private void Update()
        {
            richiButton.gameObject.SetActive(towerInfo != null && towerInfo.RichiInfo is RichiInfo richiInfo && richiInfo.State == RichiState.Ready);
            richiButtonImage.sprite = richiButtonSprite[richiButton.isDown ? 1 : 0];
        }
        public void ShowTowerStat(TowerStat stat, IReadOnlyList<HaiSpec> doraList)
        {
            towerStat = stat;
            backGround.SetActive(true);
            textParent.SetActive(true);
            ClickStatButton.SetActive(true);
            richiButton.gameObject.SetActive(true);
            ApplyTowerStatImage(doraList);
            ApplyTowerStatText();
        }

        public void RemoveTowerStat()
        {
            backGround.SetActive(false);
            SetHaisLayers(0);
            textParent.SetActive(false);
            ClickStatButton.SetActive(false);
            richiButton.gameObject.SetActive(false);
            yakusBackGround.SetActive(false);
        }

        private Image[] SetImageLayers(Transform t, int n)
        {
            var images = new Image[n];

            int childNum = t.childCount;

            var backGround = t.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(backGround, t);

            int newChildNum = t.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = t.GetChild(i);
                tmp.gameObject.SetActive(true);
                images[i] = tmp.GetComponent<Image>();
                images[i].rectTransform.anchoredPosition = Vector2.zero;
            }

            for (int i = n; i < newChildNum; i++) t.GetChild(i).gameObject.SetActive(false);

            return images;
        }

        private Transform[] SetHaisLayers(int n)
        {
            var transforms = new Transform[n];

            int childNum = imageParent.childCount;

            var haisImage = imageParent.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(haisImage, imageParent);

            int newChildNum = imageParent.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = imageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                transforms[i] = tmp;
            }

            for (int i = n; i < newChildNum; i++) imageParent.GetChild(i).gameObject.SetActive(false);

            return transforms;
        }

        private void ApplyTowerStatImage(IReadOnlyList<HaiSpec> doraList)
        {
            towerInfo = towerStat.TowerInfo;
            if (towerInfo == null)
            {
                SetHaisLayers(0);
                return;
            }

            int allHaisCount = towerInfo.Hais.Count, towerCount = 0;

            //중형, 완성 (치또이, 국사무쌍) 타워라면 마디 타워마다 간격 조정
            List<int> towerEndIndex = new();
            if (!(towerInfo is SingleHaiInfo or ToitsuInfo or
                ShuntsuInfo or KoutsuInfo or KantsuInfo))
            {
                var yakuHolderInfo = (YakuHolderInfo)towerInfo;

                int index, sum = 0;
                foreach (var info in yakuHolderInfo.MentsuInfos)
                {
                    index = info switch
                    {
                        ToitsuInfo => 2,
                        KantsuInfo => 4,
                        _ => 3,
                    };
                    sum += index;
                    towerEndIndex.Add(sum);
                }
            }

            var transforms = SetHaisLayers(allHaisCount);

            float prePos, gap;

            for (int i = 0; i < transforms.Length; i++)
            {
                //이미지 출력
                var type = towerInfo.Hais[i].Spec.HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? towerInfo.Hais[i].Spec.Number + 1
                    : towerInfo.Hais[i].Spec.Number;
                var images = SetImageLayers(transforms[i], 2);

                string backgroundHaiType = towerInfo.Hais[i].IsFuroHai ? "FuroHai1" : "BackgroundHai1";

                images[0].sprite = Tower.SingleMentsuSpriteDict[backgroundHaiType];
                images[0].color = new Color(1, 1, 1, 1);
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];

                if (doraList.Contains(towerInfo.Hais[i].Spec))
                {
                    images[0].color = new Color(1, 0.8f, 0.8f, 1);
                }

                //거리 조정
                var t = (RectTransform)transforms[i];

                gap = mentsuGap;
                t.eulerAngles = Vector3.zero;

                if (towerInfo.Hais[i].IsFuroHai)
                {
                    gap += haiFuroGap;
                    t.eulerAngles = new Vector3(0, 0, 90f);
                }

                if (i == 0)
                {
                    t.anchoredPosition = new Vector2(startingMentsuPos, 0);
                    continue;
                }

                if (towerEndIndex.Count > 0)
                    if (i == towerEndIndex[towerCount])
                    {
                        gap += towerGap;
                        towerCount++;
                    }

                prePos = ((RectTransform)transforms[i - 1]).anchoredPosition.x;

                t.anchoredPosition = new Vector2(prePos + gap, 0);

            }

            //화면 넘어가는 경우
            int cnt = 0;
            while (((RectTransform)transforms[transforms.Length - 1]).anchoredPosition.x > 9f)
            {
                float startPos = ((RectTransform)transforms[0]).anchoredPosition.x;
                foreach (RectTransform t in transforms)
                {
                    float distance = t.anchoredPosition.x - startPos;
                    t.anchoredPosition = new Vector2(distance * compressionRatio + startingMentsuPos, 0);
                }

                if (cnt++ > 1000) return;
            }
        }

        public void SetYakuText()
        {
            isYakuTextEnabled = true;

            yakusBackGround.SetActive(true);

            yakusText.text = "";

            if (towerInfo is SingleHaiInfo or ToitsuInfo or 
                ShuntsuInfo or KoutsuInfo or KantsuInfo)
            {
                isYakuTextEnabled = false;

                yakusBackGround.SetActive(false);
            }
            else
            {
                var yakuList = ((YakuHolderInfo)towerInfo).YakuList;

                if (yakuList.Count == 0)
                {
                    isYakuTextEnabled = false;

                    yakusBackGround.SetActive(false);

                    return;
                }

                int cnt = 0;
                foreach (var yaku in yakuList)
                {
                    if (cnt > 0) yakusText.text += "\n";
                    yakusText.text += "" + yaku.Name;
                    cnt++;
                }
            }
        }

        public void RemoveYakuText()
        {
            isYakuTextEnabled = false;
            yakusBackGround.SetActive(false);
        }

        private void ApplyTowerStatText()
        {
            attackText.text = towerStat.FinalStat.Damage.ToString();
            attackSpeedText.text = towerStat.FinalStat.AttackSpeed.ToString();
            criticalChanceText.text = towerStat.FinalStat.CritChance * 100 + "%";
            criticalMutiplierText.text = towerStat.FinalStat.CritDamage * 100 + "%";
            damageAmountText.text = numberFormatter(towerStat.TowerInfo.TotalDamage);
        }

        private string numberFormatter(float number)
        {
            if (number>1000000000000)
                return string.Format("{0:F1}",number/1000000000000).ToString() + "T";
            else if (number>1000000000)
                return string.Format("{0:F1}",number/1000000000).ToString() + "B";
            else if (number>1000000)
                return string.Format("{0:F1}",number/1000000).ToString() + "M";
            else if (number > 1000)
                return string.Format("{0:F0}",number/1000).ToString() + "K";
            else
                return number.ToString();
        }
    }
}
