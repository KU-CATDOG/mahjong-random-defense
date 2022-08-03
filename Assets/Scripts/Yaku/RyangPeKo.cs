using System.Linq;

namespace MRD
{
    public class RyangPeKoChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "RyangPeKo";
        public string[] OptionNames => new[] { nameof(RyangPeKoStatOption), nameof(RyangPeKoOption), nameof(RyangPeKoImageOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.Where(x => x is ShuntsuInfo) // 슌쯔만 필터링
                .GroupBy(x => x as ShuntsuInfo) // 중복 제거
                .Count(g => g.Count() > 1) == 2; // 원래 리스트에서 count 찾기
        }
    }
}
