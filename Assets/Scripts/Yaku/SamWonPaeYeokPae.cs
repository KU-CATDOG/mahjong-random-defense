using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPae";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //삼원패 역패 : 몸통 중 하나가 삼원패 커쯔
            return holder.MentsuInfos.Where(x => x is KoutsuInfo or KantsuInfo).
                Any(x => x.Hais[0].Spec.HaiType == HaiType.Sangen);
        }
    }
}
