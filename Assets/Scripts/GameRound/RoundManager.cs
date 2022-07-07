using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class RoundManager : Singleton<RoundManager>
    {
        public RoundNum round { get; private set; }
    }

    public struct RoundNum
    {
        public int season { get; private set; }
        public int wind { get; private set; }
        public int number { get; private set; }

        // 게임 종료 시 true
        public bool NextRound()
        {
            number++;
            if (number > 3)
            {
                number = 0;
                wind++;
            }
            if (wind > 3)
            {
                wind = 0;
                season++;
            }
            if (season > 3)
            {
                return true;
            }
            return false;
        }
    }
}