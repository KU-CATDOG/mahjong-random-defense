using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class TowerStatImageController : MonoBehaviour
    {
        private TowerStat towerStat;

        private TowerInfo towerInfo;

        [SerializeField]
        private Transform imageParent;
        [SerializeField]
        private float startingMentsuPos;
        [SerializeField]
        private float mentsuGap;
        [SerializeField]
        private float towerNormalGap;
        [SerializeField]
        private float towerFuroGap;
        [SerializeField]
        private float haiFuroFrontGap;
        [SerializeField]
        private float haiFuroBackGap;

        [ContextMenu("Test")]
        void test()
        {
            
            var tmp1 = new Hai(1, new HaiSpec(HaiType.Pin, 2));
            tmp1.IsFuroHai = true;

            var tmp2 = new Hai(1, new HaiSpec(HaiType.Sou, 8));
            tmp2.IsFuroHai = true;

            SetTowerStat(new TowerStat(new TripleTowerInfo(
                new ToitsuInfo(
                           new Hai(1, new HaiSpec(HaiType.Wan, 1)),
                           new Hai(1, new HaiSpec(HaiType.Wan, 1))
                 ), new ShuntsuInfo(
                           new Hai(1, new HaiSpec(HaiType.Sou, 7)),
                           tmp2,
                           new Hai(1, new HaiSpec(HaiType.Sou, 9))
                 ), new ShuntsuInfo(
                           new Hai(1, new HaiSpec(HaiType.Pin, 1)),
                           new Hai(1, new HaiSpec(HaiType.Pin, 2)),
                           new Hai(1, new HaiSpec(HaiType.Pin, 3))
                 )

                )));
            
            /*
            SetTowerStat(new TowerStat(new ToitsuInfo(
                new Hai(1, new HaiSpec(HaiType.Pin, 2)),
                new Hai(1, new HaiSpec(HaiType.Pin, 2))
                )));
            */
            /*
            towerStats.Add(new TowerStat(new SingleHaiInfo
                (new Hai(1, new HaiSpec(HaiType.Kaze, 1)))
                ));*/

            /*
             var tmp1 = new Hai(1, new HaiSpec(HaiType.Pin, 2));
            tmp1.IsFuroHai = true;
            
            SetTowerStat(new TowerStat(new KoutsuInfo(
                new Hai(1, new HaiSpec(HaiType.Pin, 2)),
                new Hai(1, new HaiSpec(HaiType.Pin, 2)),
                tmp1
                )));
            */
            /*
            var tmp = new Hai(3, new HaiSpec(HaiType.Sou, 3));
            tmp.IsFuroHai = true;

            towerStats.Add(new TowerStat(new ShuntsuInfo(
                new Hai(1, new HaiSpec(HaiType.Sou, 1)),
                new Hai(2, new HaiSpec(HaiType.Sou, 2)),
                new Hai(3, new HaiSpec(HaiType.Sou, 3)))
                ));
            */

            ApplyTowerImage();
        }

        public void SetTowerStat(TowerStat stat)
        {
            towerStat = stat;
        }


        //댐지, 공속, 치명, 치피, 이전 라운드에 준 딜량 표시
        //클릭하면 창 길어지며 역 목록 출력

        private Image[] SetImageLayers(Transform t, int n)
        {
            Image[] images = new Image[n];

            int childNum = t.childCount;

            Transform backGround = t.GetChild(0);

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

            for (int i = n; i < newChildNum; i++)
            {
                t.GetChild(i).gameObject.SetActive(false);
            }

            return images;
        }

        private Transform[] SetHaisLayers(int n)
        {
            Transform[] transforms = new Transform[n];

            int childNum = imageParent.childCount;

            Transform haisImage = imageParent.GetChild(0);


            for (int i = childNum; i < n; i++) Instantiate(haisImage, imageParent);

            int newChildNum = imageParent.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = imageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                transforms[i] = tmp;
            }

            for (int i = n; i < newChildNum; i++)
            {
                imageParent.GetChild(i).gameObject.SetActive(false);
            }

            return transforms;
        }

        public void ApplyTowerImage()
        {
            towerInfo = towerStat.TowerInfo;
            if (towerInfo == null)
            {
                SetHaisLayers(0);
                return;
            }
            int allHaisCount = towerInfo.Hais.Count, towerCount = 0;

            List<int> towerEndIndex = new();

            if (towerInfo is TripleTowerInfo)
            {
                var tripleTowerInfo = (TripleTowerInfo)towerInfo;
                int index, sum = 0;
                foreach(var info in tripleTowerInfo.MentsuInfos)
                {
                    index = info switch
                    {
                        ToitsuInfo => 2,
                        KantsuInfo => 4,
                        _ => 3
                    };
                    sum += index;
                    towerEndIndex.Add(sum);
                    print(sum);
                }
            }

            Transform[] transforms = SetHaisLayers(allHaisCount);
            print("allHaisCount : " + allHaisCount);

            var tPos = startingMentsuPos;

            for (int i = 0; i < transforms.Length; i++)
            {
                //이미지 출력
                HaiType type = towerInfo.Hais[i].Spec.HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen ?
                    towerInfo.Hais[i].Spec.Number + 1 :
                    towerInfo.Hais[i].Spec.Number;
                print($"transform Count {transforms.Length}");
                Image[] images = SetImageLayers(transforms[i], 2);
                images[0].sprite = Tower.SingleMentsuSpriteDict[$"BackgroundHai1"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type.ToString() + number.ToString()];

                //거리 조정
                var t = (RectTransform)transforms[i];
                tPos += i == 0 ? 0 : mentsuGap;

                if (towerInfo.Hais[i].IsFuroHai)
                {
                    tPos += haiFuroFrontGap;
                    t.rotation = Quaternion.Euler(0, 0, 90f);
                }

                if (i != 0)
                {
                    if (towerInfo.Hais[i - 1].IsFuroHai)
                    {
                        tPos += haiFuroBackGap;
                    }
                }

                if (towerEndIndex.Count > 0)
                {
                    if (i == towerEndIndex[towerCount])
                    {
                        tPos += towerNormalGap;
                        towerCount++;
                    }
                }

                t.anchoredPosition = new Vector2(tPos, 0);
            }
        }
    }
}