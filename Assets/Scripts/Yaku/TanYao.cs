using System.Linq;

namespace MRD
{
    public class TanYaoChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "TanYao";

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.SelectMany(x => x.Hais).All(x => !x.Spec.IsYaochu);
        }
    }
}
