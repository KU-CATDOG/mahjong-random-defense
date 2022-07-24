using System.Linq;

namespace MRD
{
    public class ShuKantSuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ShuKantSu";
        public string[] OptionNames => new string[] { nameof(ShuKantSuStatOption), nameof(ShuKantSuOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Count(x => x is KantsuInfo) == 4;
        }
    }

}
