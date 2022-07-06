using System.Linq;

namespace MRD
{
    public class SoSaHeeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SoSaHee";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //소사희: 풍패 몸통 3개, 머리 1개
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Kaze) == 3 && // 몸통 3개
                    holder.MentsuInfos.Count(x => (x is ToitsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Kaze) == 1; //머리 하나
        }
    }
}
