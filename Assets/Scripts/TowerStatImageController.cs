using System.Collections.Generic;
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
        private GameObject ClickStatButton;

        [SerializeField]
        private float startingMentsuPos;

        [SerializeField]
        private float mentsuGap;

        [SerializeField]
        private float towerNormalGap;

        [SerializeField]
        private float haiFuroFrontGap;

        [SerializeField]
        private float haiFuroBackGap;

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

        public void ShowTowerStat(TowerStat stat)
        {
            towerStat = stat;
            backGround.SetActive(true);
            textParent.SetActive(true);
            ClickStatButton.SetActive(true);
            ApplyTowerStatImage();
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

        private void ApplyTowerStatImage()
        {
            towerInfo = towerStat.TowerInfo;
            if (towerInfo == null)
            {
                SetHaisLayers(0);
                return;
            }

            int allHaisCount = towerInfo.Hais.Count, towerCount = 0;

            //중형 타워라면 마디 타워마다 간격 조정
            List<int> towerEndIndex = new();
            if (towerInfo is TripleTowerInfo)
            {
                var tripleTowerInfo = (TripleTowerInfo)towerInfo;
                int index, sum = 0;
                foreach (var info in tripleTowerInfo.MentsuInfos)
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

            float tPos = startingMentsuPos;

            for (int i = 0; i < transforms.Length; i++)
            {
                //이미지 출력
                var type = towerInfo.Hais[i].Spec.HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? towerInfo.Hais[i].Spec.Number + 1
                    : towerInfo.Hais[i].Spec.Number;
                var images = SetImageLayers(transforms[i], 2);
                images[0].sprite = Tower.SingleMentsuSpriteDict["BackgroundHai1"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];

                //거리 조정
                var t = (RectTransform)transforms[i];
                tPos += i == 0 ? 0 : mentsuGap;

                if (towerInfo.Hais[i].IsFuroHai)
                {
                    images[0].sprite = Tower.SingleMentsuSpriteDict["FuroHai1"];
                    tPos += haiFuroFrontGap;
                    t.rotation = Quaternion.Euler(0, 0, 90f);
                }

                if (i != 0)
                    if (towerInfo.Hais[i - 1].IsFuroHai)
                        tPos += haiFuroBackGap;

                if (towerEndIndex.Count > 0)
                    if (i == towerEndIndex[towerCount])
                    {
                        tPos += towerNormalGap;
                        towerCount++;
                    }

                t.anchoredPosition = new Vector2(tPos, 0);
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
            attackText.text = towerStat.FinalAttack.ToString();
            attackSpeedText.text = towerStat.FinalAttackSpeed.ToString();
            criticalChanceText.text = towerStat.FinalCritChance + "%";
            criticalMutiplierText.text = towerStat.FinalCritMultiplier * 100 + "%";
            //TODO : 이전 라운드에 준 딜량 표시
        }
    }
}
