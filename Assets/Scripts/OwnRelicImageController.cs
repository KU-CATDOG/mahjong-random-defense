using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class OwnRelicImageController : MonoBehaviour
    {
        [SerializeField]
        private Transform imageParent;

        [SerializeField]
        private RelicInstruction RelicData;

        [SerializeField]
        private SetRelicComponents relicInst;

        private List<ClickUI> ownRelicsClickUI = new();

        private void Update()
        {
            for(int i = 0; i < ownRelicsClickUI.Count; i++)
            {
                if (ownRelicsClickUI[i].isDown)
                {
                    ShowRelicInst(i);
                    return;
                }
            }
            RemoveRelicInst();
        }

        public void ShowOwnRelics()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        public void RemoveOwnReclics()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        public void SetOwnRelic()
        {
            var ownRelics = RoundManager.Inst.RelicManager.OwnRelics;

            var transforms = SetRelicsLayers(ownRelics.Count);

            var relicSprites = ResourceDictionary.GetAll<Sprite>("Icons/relics");

            for (int i = 0; i < transforms.Length; i++)
            {
                //이미지 출력
                var images = SetImageLayers(transforms[i], 2);

                images[0].sprite = relicSprites[(int)ownRelics[i].Rank];
                images[1].sprite = RoundManager.Inst.RelicManager.relicSpriteDic[ownRelics[i].Name];

                if (ownRelics[i].Amount > 1)
                {
                    SetAmountText(transforms[i], ownRelics[i].Amount);
                }
            }
        }

        private void SetAmountText(Transform t, int amount)
        {
            var amountText = t.GetChild(t.childCount - 1).GetComponent<Text>();
            amountText.gameObject.SetActive(true);
            amountText.text = "X" + amount.ToString();
            if (amount >= 10)
            {
                for (int i = 0; i < amount.ToString().Length - 1; i++)
                {
                    var rectText = (RectTransform)amountText.transform;
                    rectText.anchoredPosition = new Vector2(rectText.anchoredPosition.x + -0.1f, rectText.anchoredPosition.y);
                    rectText.sizeDelta = new Vector2(rectText.sizeDelta.x + 0.5f, rectText.sizeDelta.y);
                }
            }
        }

        private void ShowRelicInst(int index)
        {
            relicInst.gameObject.SetActive(true);

            var relicNum = RoundManager.Inst.RelicManager.NowRelicNum(RoundManager.Inst.RelicManager.OwnRelics[index].GetType());

            var relicInstArr = RelicData.Insts;

            var relic = relicInstArr[relicNum];

            var rankSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/relic_border");

            switch (relic.Rank)
            {
                case "S":
                    relicInst.transform.GetComponent<Image>().sprite = rankSpriteArr[3];
                    break;
                case "A":
                    relicInst.transform.GetComponent<Image>().sprite = rankSpriteArr[2];
                    break;
                case "B":
                    relicInst.transform.GetComponent<Image>().sprite = rankSpriteArr[1];
                    break;
                case "C":
                    relicInst.transform.GetComponent<Image>().sprite = rankSpriteArr[0];
                    break;
            }
            relicInst.Name.text = relicInstArr[relicNum].Name;
            relicInst.Info.text = relicInstArr[relicNum].Info;
            relicInst.RelicImage.GetComponent<Image>().sprite = relicInstArr[relicNum].Image;
        }

        private void RemoveRelicInst()
        {
            relicInst.gameObject.SetActive(false);
        }

        private Transform[] SetRelicsLayers(int n)
        {
            var transforms = new Transform[n];

            int childNum = imageParent.childCount;

            var relicsImage = imageParent.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(relicsImage, imageParent);

            int newChildNum = imageParent.childCount;

            ownRelicsClickUI.Clear();

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = imageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                ownRelicsClickUI.Add(tmp.GetComponent<ClickUI>());
                transforms[i] = tmp;
            }

            for (int i = n; i < newChildNum; i++) imageParent.GetChild(i).gameObject.SetActive(false);

            return transforms;
        }

        private Image[] SetImageLayers(Transform t, int n)
        {
            var images = new Image[n];

            int childNum = t.childCount - 1;

            var image = t.GetChild(0);

            var amountText = t.GetChild(childNum);
            amountText.gameObject.SetActive(false);

            for (int i = childNum; i < n; i++) Instantiate(image, t);

            amountText.transform.SetAsLastSibling();

            int newChildNum = t.childCount - 1;

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

