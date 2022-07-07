using System.Linq;

namespace MRD
{
    public class ToiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ToiToi";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //조건: 커쯔 4개
            return holder.MentsuInfos.Count(x => x is KoutsuInfo) == 4;
        }
    }

}
