using System.Linq;

namespace MRD
{
    public class HonNoDuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "HonNoDu";
        public string[] OptionNames => new[] { nameof(HonNoDuImageOption), nameof(HonNoDuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            // 혼노두: 모든 패가 노두패(1 혹은 9)와 자패만으로만 이루어짐
            return holder.MentsuInfos.All(x => x.Hais.All(y => y.Spec.IsYaochu)); // 모든 패가 조건을 만족하는지 확인
        }
    }
}
