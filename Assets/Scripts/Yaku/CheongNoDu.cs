using System.Linq;

namespace MRD
{
    public class CheongNoDuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongNoDu";
        public string[] OptionNames => new string[] { nameof(CheongNoDuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   // 혼노두: 모든 패가 노두패(1 혹은 9)로만 이루어짐
            return holder.MentsuInfos.All(x => x.Hais.All(y => y.Spec.IsRoutou));// 모든 패가 조건을 만족하는지 확인
        }
    }
}
