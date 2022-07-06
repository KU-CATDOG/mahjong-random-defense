using System.Linq;

namespace MRD
{
    public class SoSamWonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SoSamWon";

        public bool CheckCondition(YakuHolderInfo holder)
        {
            //조건: 삼원 3개 중 2개로 몸통(깡쯔 혹은 커쯔), 하나로 머리(또이쯔)
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Sangen) == 2 && // 몸통 2개
                   holder.MentsuInfos.Count(x => (x is ToitsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Sangen) == 1; //머리 하나
        }
    }

}
