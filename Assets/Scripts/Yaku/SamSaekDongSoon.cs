using System.Linq;

namespace MRD
{
    public class SamSaekDongSoonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamSaekDongSoon";

        public bool CheckCondition(YakuHolderInfo holder)
        {   
            return holder.MentsuInfos.Select(x => x.Hais).All(x => x is ShuntsuInfo);
        }
    }

}
