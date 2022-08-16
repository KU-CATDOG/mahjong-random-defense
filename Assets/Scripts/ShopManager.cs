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

        private void Start()
        {
            canvas.ShopButtons[0].AddListenerOnly(() =>
            {
                RelicManager.Refresh();
                RoundManager.Inst.refreshCostText.text = "" + RelicManager.RefreshCost;
            });
            canvas.ShopButtons[1].AddListenerOnly(() =>
            {
                RelicManager.BuyRelic(0);
            });
            canvas.ShopButtons[2].AddListenerOnly(() =>
            {
                RelicManager.BuyRelic(1);
            });
            canvas.ShopButtons[3].AddListenerOnly(() =>
            {
                RelicManager.BuyRelic(2);
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
            RelicManager.ResetRefreshCost();
            RoundManager.Inst.refreshCostText.text = "" + RelicManager.RefreshCost;
        }
    }
}
