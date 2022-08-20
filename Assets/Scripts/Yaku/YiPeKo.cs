using System.Linq;

namespace MRD
{
    public class YiPeKoChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "YiPeKo";

        public string[] OptionNames => new[]
            { nameof(YiPeKoImageOption), nameof(YiPeKoStatOption), nameof(YiPeKoOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            //조건: 완전히 동일한 슌쯔(연속 3개)가 두 개
            return holder.MentsuInfos.Where(x => x is ShuntsuInfo) // 슌쯔만 필터링
                .Cast<ShuntsuInfo>()
                .GroupBy(x => x.Hais[0].Spec)// 중복 제거
                .Any(g => g.Count() > 1) && holder.isMenzen; // 원래 리스트에서 count 찾기
        }
    }
}
