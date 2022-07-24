using System.Linq;

namespace MRD
{
    public class ToiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ToiToi";
        public string[] OptionNames => new string[] { nameof(ToiToiImageOption), nameof(ToiToiStatOption), nameof(ToiToiOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   
            return holder.MentsuInfos.Count(x => x is ShuntsuInfo) == 0 && holder.MentsuInfos.Count(x => x is ToitsuInfo) < 2;
        }
    }

}
