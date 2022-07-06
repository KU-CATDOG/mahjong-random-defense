using System.Linq;

namespace MRD
{
    public class ChiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ChiToi";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //치또이: 또이쯔 7개
            return holder.MentsuInfos.Count(x => (x is ToitsuInfo)) == 7;
        }
    }
}
