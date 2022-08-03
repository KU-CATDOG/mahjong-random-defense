using System.Linq;

namespace MRD
{
    public class DaeSaHeeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "DaeSaHee";
        public string[] OptionNames => new[] { nameof(DaeSaHeeStatOption), nameof(DaeSaHeeOption), nameof(DaeSaHeeImageOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            //대사희: 풍패 몸통 4개
            return holder.MentsuInfos.Count(
                x => x is KoutsuInfo or KantsuInfo && x.Hais[0].Spec.HaiType == HaiType.Kaze) == 4; // 몸통 4개
        }
    }
}
