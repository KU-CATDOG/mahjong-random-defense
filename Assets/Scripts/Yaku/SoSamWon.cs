using System.Linq;

namespace MRD
{
    public class SoSamWonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SoSamWon";

        public string[] OptionNames => new[]
            { nameof(SoSamWonImageOption), nameof(SoSamWonStatOption), nameof(SoSamWonOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Count(x => x.Hais[0].Spec.HaiType == HaiType.Sangen) == 3 &&
                   holder.MentsuInfos.Count(x => x is ToitsuInfo) < 2;
        }
    }
}
