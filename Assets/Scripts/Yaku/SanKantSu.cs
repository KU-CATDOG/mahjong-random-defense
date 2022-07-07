﻿using System.Linq;

namespace MRD
{
    public class SanKantSuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SanKantSu";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //조건: 깡쯔 3개
            return holder.MentsuInfos.Count(x => x is KantsuInfo) == 3;
        }
    }

}
