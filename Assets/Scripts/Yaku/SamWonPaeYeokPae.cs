using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPae";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {   //삼원패 역패 : 삼원패로 된 머리 or 몸통이 2개 이하여야 함
            //              3개 -> 소삼원 or 대삼원
            return holder.MentsuInfos.Count(x => x.Hais[0].Spec.HaiType == HaiType.Sangen) < 3;
        }
    }
}
