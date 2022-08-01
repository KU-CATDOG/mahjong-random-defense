using System.Linq;

namespace MRD
{
    public class ChanTaChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ChanTa";
        public string[] OptionNames => new[] { nameof(ChantaImageOption), nameof(ChanTaStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.All(x => x.Hais.Any(y => y.Spec.IsYaochu));
        }
    }
}
