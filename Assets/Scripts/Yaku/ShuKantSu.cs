using System.Linq;

namespace MRD
{
    public class ShuKantSuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ShuKantSu";
        public string[] OptionNames => new string[] { nameof(ShuKantSuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   //Á¶°Ç: ±øÂê 4°³
            return holder.MentsuInfos.Count(x => x is KantsuInfo) == 4;
        }
    }

}
