using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public void ResetShop()
        {
            RelicManager.Refresh(true);
            RelicManager.ResetRefreshCost();
        }
    }
}
