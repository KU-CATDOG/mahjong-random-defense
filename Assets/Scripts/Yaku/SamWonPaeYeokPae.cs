using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeBaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPaeBaek";
        public string[] OptionNames => new string[] { nameof(SamWonPaeYeokPaeImageOption), nameof(SamWonPaeYeokPaeStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   
            return holder.MentsuInfos.Any(x => x is KoutsuInfo or KantsuInfo && x.Hais[0].Spec.Equals(HaiType.Sangen, 0));
        }
    }
    public class SamWonPaeYeokPaeBalChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPaeBal";
        public string[] OptionNames => new string[] { nameof(SamWonPaeYeokPaeImageOption), nameof(SamWonPaeYeokPaeStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Any(x => x is KoutsuInfo or KantsuInfo && x.Hais[0].Spec.Equals(HaiType.Sangen, 1));
        }
    }
    public class SamWonPaeYeokPaeJoongChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPaeJoong";
        public string[] OptionNames => new string[] { nameof(SamWonPaeYeokPaeImageOption), nameof(SamWonPaeYeokPaeStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Any(x => x is KoutsuInfo or KantsuInfo && x.Hais[0].Spec.Equals(HaiType.Sangen, 2));
        }
    }
}
