using System.Linq;

namespace MRD
{
    public class SoSamWonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SoSamWon";
        public string[] OptionNames => new string[] { nameof(SoSamWonImageOption), nameof(SoSamWonStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Count(x => x.Hais[0].Spec.HaiType == HaiType.Sangen) == 3 && holder.MentsuInfos.Count(x => x is ToitsuInfo) == 1;
        }
    }

}
