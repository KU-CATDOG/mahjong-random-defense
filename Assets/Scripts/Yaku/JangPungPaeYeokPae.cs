using System.Linq;

namespace MRD
{
    public class JangPungPaeYeokPaeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "JangPungPaeYeokPae";

        public string[] OptionNames => new[]
        {
            nameof(JangPungPaeYeokPaeImageOption), nameof(JangPungPaeYeokPaeStatOption),
            nameof(JangPungPaeYeokPaeOption),
        };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            //장풍패 역패 : 몸통 중 하나가 장풍패 커쯔
            return holder.MentsuInfos.Where(x => x is KoutsuInfo or KantsuInfo).Any(x =>
                x.Hais[0].Spec.HaiType == HaiType.Kaze &&
                x.Hais[0].Spec.Number == RoundManager.Inst.round.wind);
        }
    }
}
