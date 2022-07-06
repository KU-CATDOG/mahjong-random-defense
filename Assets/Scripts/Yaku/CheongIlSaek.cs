using System.Linq;

namespace MRD
{
    public class CheongIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongIlSaek";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //청일색: 몸통 4개와 머리 1개 모두 동일 종류 
            HaiType cmpbase = holder.MentsuInfos[0].Hais[0].HaiType; // 비교 기준으로 첫 멘쯔의 모양 설정
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == cmpbase) == 4 && // 몸통 4개
                   holder.MentsuInfos.Count(x => (x is ToitsuInfo) && x.Hais[0].Spec.HaiType == cmpbase) == 1; //머리 하나
        }
    }
}
