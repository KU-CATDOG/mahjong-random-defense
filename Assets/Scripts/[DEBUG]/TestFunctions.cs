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

        private void Update()
        {
            if (rm.HAI_CHEAT)
            {
                if (Input.GetKeyDown(KeyCode.Q)) rm.HAI_CHEAT_SPEC_TYPE = HaiType.Wan;
                if (Input.GetKeyDown(KeyCode.W)) rm.HAI_CHEAT_SPEC_TYPE = HaiType.Sou;
                if (Input.GetKeyDown(KeyCode.E)) rm.HAI_CHEAT_SPEC_TYPE = HaiType.Pin;
                if (Input.GetKeyDown(KeyCode.R)) rm.HAI_CHEAT_SPEC_TYPE = HaiType.Kaze;
                if (Input.GetKeyDown(KeyCode.T)) rm.HAI_CHEAT_SPEC_TYPE = HaiType.Sangen;

                for (int i = 0; i < 10; i++)
                {
                    if (Input.GetKeyDown((KeyCode)(i + 48)) || Input.GetKeyDown((KeyCode)(i + 256)))
                    {
                        rm.HAI_CHEAT_SPEC_NUM = i;
                        rm.Grid.RandomTsumo();
                    }
                }
            }
        }
    }
}
