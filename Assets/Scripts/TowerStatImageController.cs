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
        private float mentsuGap;

        [SerializeField]
        private float haiGap;

        [SerializeField]
        private float furoGap;

        [SerializeField]
        private float paddingGap;

        public Text attackText;
        public Text attackSpeedText;
        public Text criticalChanceText;
        public Text criticalMutiplierText;
        public Text damageAmountText;

        public Text yakusText;

        private Dictionary<string, string> yakuKorName = new();

        public IReadOnlyDictionary<string, string> YakuKorName => yakuKorName;

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

            SetYakuKorName();
        }
        private void Update()
        {
            richiButton.gameObject.SetActive(backGround.activeSelf && towerInfo != null && towerInfo.RichiInfo is RichiInfo richiInfo && richiInfo.State == RichiState.Ready);
            richiButtonImage.sprite = richiButtonSprite[richiButton.isDown ? 1 : 0];
        }
        public void ShowTowerStat(TowerStat stat, IReadOnlyList<HaiSpec> doraList)
        {
            towerStat = stat;
            backGround.SetActive(true);
            textParent.SetActive(true);
            ClickStatButton.SetActive(true);
            ApplyTowerStatImage(doraList);
            ApplyTowerStatText();
        }

        public void RemoveTowerStat()
        {
            backGround.SetActive(false);
            SetHaisLayers(0);
            textParent.SetActive(false);
            ClickStatButton.SetActive(false);
            yakusBackGround.SetActive(false);
        }

        private void SetYakuKorName()
        {
            var yakus = ResourceDictionary.GetAll<YakuInstructionScriptable>("Instruction/Yaku");

            foreach(var yaku in yakus)
            {
                if (yaku.name == "SamWonPaeYeokPae")
                {
                    yakuKorName.Add(yaku.name + "Baek", yaku.OfficialName);
                    yakuKorName.Add(yaku.name + "Bal", yaku.OfficialName);
                    yakuKorName.Add(yaku.name + "Joong", yaku.OfficialName);
                    continue;
                }
                yakuKorName.Add(yaku.name, yaku.OfficialName);
            }
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

            int allHaisCount = towerInfo.Hais.Count;

            var transforms = SetHaisLayers(allHaisCount);

            float currentX = paddingGap;

            System.Action<Transform, Hai> setHai = (trns, hai) =>
            {
                var type = hai.Spec.HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? hai.Spec.Number + 1
                    : hai.Spec.Number;
                var images = SetImageLayers(trns, 2);

                string backgroundHaiType = hai.IsFuroHai ? "FuroHai1" : "BackgroundHai1";

                images[0].sprite = Tower.SingleMentsuSpriteDict[backgroundHaiType];
                images[0].color = doraList.Contains(hai.Spec) ? new Color(1, 0.8f, 0.8f, 1) : new Color(1, 1, 1, 1);
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];
                images[1].color = doraList.Contains(hai.Spec) ? new Color(1, 0.8f, 0.8f, 1) : new Color(1, 1, 1, 1);

                //거리 조정
                var t = (RectTransform)trns;

                t.eulerAngles = Vector3.zero;

                if (hai.IsFuroHai)
                {
                    currentX += furoGap;
                    t.eulerAngles = new Vector3(0, 0, 90f);
                }

                t.anchoredPosition = new Vector2(currentX, 0);
                currentX += haiGap;
            };

            if (towerInfo is YakuHolderInfo yInfo)
            {
                List<MentsuInfo> orderInfo = yInfo.MentsuInfos.OrderBy(x => x.Hais[0].Spec.GetHashCode()).ToList();
                int idx = 0;
                foreach (var m in orderInfo)
                {
                    foreach (var h in m.Hais)
                    {
                        setHai(transforms[idx++], h);
                    }
                    currentX += mentsuGap;
                }
            }
            else
            {
                int idx = 0;
                foreach (var h in towerInfo.Hais)
                {
                    setHai(transforms[idx++], h);
                }
            }

            //화면 넘어가는 경우
            if (((RectTransform)transforms[transforms.Length - 1]).anchoredPosition.x > 10f - paddingGap)
            {
                float ratio = (10f - paddingGap * 2) / (((RectTransform)transforms[transforms.Length - 1]).anchoredPosition.x - paddingGap);
                foreach (RectTransform t in transforms)
                {
                    t.anchoredPosition = new Vector2((t.anchoredPosition.x - paddingGap) * ratio + paddingGap, 0);
                }
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
                    yakusText.text += "" + yakuKorName[yaku.Name];
                    cnt++;
                }

                var textTransform = yakusText.gameObject.transform as RectTransform;
                textTransform.sizeDelta = new Vector2(15f, 1.25f * yakuList.Count);

                var exceedCount = yakuList.Count > 4 ? yakuList.Count - 4 : 0;
                
                var backGroundTransform = yakusBackGround.transform as RectTransform;
                backGroundTransform.anchoredPosition = new Vector2(5, 3.125f + 0.4f * exceedCount);
                backGroundTransform.sizeDelta = new Vector2(10f, 3.125f + 0.8f * exceedCount);
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
