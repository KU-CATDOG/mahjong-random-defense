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

            canvas.ShopButtons[4].AddListenerOnly(() =>
            {
                RoundManager.Inst.Grid.State = EditState.Idle;
                RoundManager.Inst.Grid.ResetGrid();
                if (RoundManager.Inst.Grid.doraList.isShowingDora)
                {
                    RoundManager.Inst.Grid.doraList.ResetDoraImage();
                }
                canvas.shopBlackScreen.SetActive(false);
                canvas.BlackScreen.SetActive(true);
            });
        }

        private void Update()
        {
            if (canvas.ShopButton.isDown)
                SetButtonImage(1, canvas.ShopButton);
            else
                SetButtonImage(0, canvas.ShopButton);

            if (canvas.ShopButtons[4].isDown)
                SetButtonImage(3, canvas.ShopButtons[4]);
            else
                SetButtonImage(2, canvas.ShopButtons[4]);

            if (canvas.ShopButtons[0].isDown)
                SetButtonImage(9, canvas.ShopButtons[0]);
            else
                SetButtonImage(8, canvas.ShopButtons[0]);
        }

        private void SetButtonImage(int num, ClickUI button)
        {
            var shopButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/extra_button");

            button.GetComponent<Image>().sprite = shopButtonSpriteArr[num];
                
        }
        public void ResetShop()
        {
            RelicManager.Refresh(true);
            RelicManager.ResetRefreshCost();
            RoundManager.Inst.refreshCostText.text = "" + RelicManager.RefreshCost;
        }
    }
}
