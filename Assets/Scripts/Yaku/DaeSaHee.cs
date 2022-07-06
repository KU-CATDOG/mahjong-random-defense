using System.Linq;

namespace MRD
{
    public class DaeSaHeeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "DaeSaHee";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //소사희: 풍패 몸통 4개
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Kaze) == 4;// 몸통 4개
        }
    }
}
