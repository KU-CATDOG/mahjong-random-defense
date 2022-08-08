using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MRD
{
    public class TestFunctions : MonoBehaviour
    {
        private RoundManager rm => RoundManager.Inst;
        [ContextMenu("RefreshShop")]
        private void RefreshShop()
        {
            rm.RelicManager.Refresh(true);
            Debug.Log(rm.RelicManager.Shop[0].Name + " " + rm.RelicManager.Shop[1].Name + " " + rm.RelicManager.Shop[2].Name);
        }

        [ContextMenu("Buy1")]
        private void Buy1() => rm.RelicManager.BuyRelic(0);
        [ContextMenu("Buy2")]
        private void Buy2() => rm.RelicManager.BuyRelic(1);
        [ContextMenu("Buy3")]
        private void Buy3() => rm.RelicManager.BuyRelic(2);



        [ContextMenu("MONEY_CHEAT")]
        private void MoneyCheat()
        {
            rm.PlusTsumoToken(100);
        }
    }
}
