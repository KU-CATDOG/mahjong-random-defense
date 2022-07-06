using System.Linq;

namespace MRD
{
    public class CheongIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongIlSaek";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //청일색: 수패가 모두 같은 모양
            HaiType cmpbase = holder.MentsuInfos[0].Hais[0].Spec.HaiType; // 비교 기준으로 첫 멘쯔의 모양 설정
            if (cmpbase is not HaiType.Wan or HaiType.Pin or HaiType.Sou) return false; // 수패가 아니면 false 리턴
            return holder.MentsuInfos.All(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == cmpbase);// 모두 비교대상과 같은지 체크
        }
    }
}
