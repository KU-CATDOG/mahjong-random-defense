using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPae";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //»ï¿øÆÐ ¿ªÆÐ : ¸öÅë Áß ÇÏ³ª°¡ »ï¿øÆÐ Ä¿Âê
            return holder.MentsuInfos.Where(x => x is KoutsuInfo or KantsuInfo).
                Any(x => x.Hais[0].Spec.HaiType == HaiType.Sangen);
        }
    }
}
