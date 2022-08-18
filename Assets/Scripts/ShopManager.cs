using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class ShopManager : MonoBehaviour
    {
        private RelicManager RelicManager => RoundManager.Inst.RelicManager;
        [SerializeField]
        private CanvasComponents canvas;
        [SerializeField]
        private RelicInstruction RelicInst;
        [SerializeField]
        private OwnRelicImageController ownRelicImageController;

        public GameObject[] ShopBuyButtons;
        public Text[] RelicMoney;

        private void Start()
        {
            RelicInst.MakeRelicSpriteDic();

            canvas.ShopButtons[0].AddListenerOnly(() =>
            {
                RelicManager.Refresh();
                shopimage();
                RoundManager.Inst.refreshCostText.text = "" + RelicManager.RefreshCost;
            });
            canvas.ShopButtons[1].AddListenerOnly(() =>
            {
                if (RelicManager.BuyRelic(0))
                {
                    ShopBuyButtons[0].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[4];
                    ownRelicImageController.SetOwnRelic();
                }
            });
            canvas.ShopButtons[2].AddListenerOnly(() =>
            {
                if (RelicManager.BuyRelic(1))
                {
                    ShopBuyButtons[1].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[4];
                    ownRelicImageController.SetOwnRelic();
                }
            });
            canvas.ShopButtons[3].AddListenerOnly(() =>
            {
                if (RelicManager.BuyRelic(2))
                {
                    ShopBuyButtons[2].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[4];
                    ownRelicImageController.SetOwnRelic();
                }
            });
        }

        private void Update()
        {
            SetShopButtonImage();
            SetReroleImage();
        }

        private void SetShopButtonImage()
        {
            var shopButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/top_buttons");
            canvas.ShopButton.GetComponent<Image>().sprite = canvas.ShopButton.isDown ? shopButtonSpriteArr[3] : shopButtonSpriteArr[2];                
        }

        private void SetReroleImage()
        {
            var ButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/refresh_button");
            canvas.ShopButtons[0].GetComponent<Image>().sprite = canvas.ShopButtons[0].isDown ? ButtonSpriteArr[1] : ButtonSpriteArr[0];
        }
        public void ResetShop()
        {
            RelicManager.Refresh(true);
            shopimage();
            RelicManager.ResetRefreshCost();
            RoundManager.Inst.refreshCostText.text = "" + RelicManager.RefreshCost;
        }

        public void shopimage()
        {
            int relicnum = 0;
            for (int i = 0; i < 3; i++)
            {
                relicnum = RelicManager.NowRelicNum(RelicManager.Shop[i]);;

                var c = ShopBuyButtons[i].GetComponent<SetRelicComponents>();

                switch (RelicInst.Insts[relicnum].Rank)
                {
                    case "S":
                        ShopBuyButtons[i].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[3];
                        break;
                    case "A":
                        ShopBuyButtons[i].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[2];
                        break;
                    case "B":
                        ShopBuyButtons[i].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[1];
                        break;
                    case "C":
                        ShopBuyButtons[i].GetComponent<Image>().sprite = RelicInst.rankSpriteArr[0];
                        break;
                }
                c.Name.text = RelicInst.Insts[relicnum].Name;
                c.Info.text = RelicInst.Insts[relicnum].Info;
                c.RelicImage.GetComponent<Image>().sprite = RelicInst.Insts[relicnum].Image;
                RelicMoney[i].text = "" + RelicManager.RelicMoney(i);
            }
        }
    }
}
