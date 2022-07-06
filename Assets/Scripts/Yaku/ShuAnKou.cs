using System.Linq;

namespace MRD
{
    public class ShuAnKouChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ShuAnKou";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //쓰안커: 멘젠 커쯔나 캉쯔 4개
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.IsMenzen) == 4;
        }
    }
}
