using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class DoraList : MonoBehaviour
    {
        private readonly List<HaiSpec> doraList = new();

        public IReadOnlyList<HaiSpec> GetDoraList => doraList;

        public int maxDoraCount => 1 + RoundManager.Inst.RelicManager[typeof(DoraRelic)];

        [SerializeField]
        private Transform imageParent;

        [SerializeField]
        private CanvasComponents canvas;

        private bool isShowingDora;

        private HaiType[] haiTypes =
        {
            HaiType.Kaze,
            HaiType.Pin,
            HaiType.Sangen,
            HaiType.Sou,
            HaiType.Wan,
        };

        private void Start()
        {
            canvas.DoraButton.AddListenerOnly(() => SetDoraImage());
        }

        private void Update()
        {
            SetButtonImage();
        }

        [ContextMenu("Add Dora")]
        public void AddDora()
        {
            for(int i = 0; i < maxDoraCount; i++){
                HaiType haiType = haiTypes[Random.Range(0, haiTypes.Length)];
                int number = haiType switch
                {
                    HaiType.Kaze => Random.Range(0, 4),
                    HaiType.Sangen => Random.Range(0, 3),
                    _ => Random.Range(1, 10)
                };
                doraList.Add(new HaiSpec(haiType, number));
            }
        }

        [ContextMenu("Reset Dora")]
        public void ResetDoraList()
        {
            SetHaisLayers(0);
            doraList.Clear();
        }

        private void SetDoraImage()
        {
            if (isShowingDora)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                isShowingDora = false;
                return;
            }

            isShowingDora = true;

            transform.GetChild(0).gameObject.SetActive(true);

            var transforms = SetHaisLayers(doraList.Count);

            for (int i = 0; i < transforms.Length; i++)
            {
                //간격 조정
                ((RectTransform)transforms[i]).anchoredPosition = new Vector2(-4 + i * 1.5f, -0.15f);

                //이미지 출력
                HaiType type = doraList[i].HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? doraList[i].Number + 1
                    : doraList[i].Number;
                var images = SetImageLayers(transforms[i], 2);
                images[0].sprite = Tower.SingleMentsuSpriteDict["BackgroundHai1"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];
            }
        }

        private void SetButtonImage()
        {
            var doraButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/extra_button");

            canvas.DoraButton.GetComponent<Image>().sprite = canvas.DoraButton.isDown == true ?
                doraButtonSpriteArr[7] : doraButtonSpriteArr[6];
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
    }
}
