using System.Linq;

namespace MRD
{
    public class SamSaekDongSoonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamSaekDongSoon";
        public string[] OptionNames => new string[] { nameof(SamSaekDongSoonStatOption), nameof(SamSaekDongSoonImageOption), nameof(SamSaekDongSoonOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int mask = 0;
            foreach (var shuntsu in holder.MentsuInfos.Where(x => x is ShuntsuInfo).Cast<ShuntsuInfo>())
            {
                mask |= shuntsu.HaiType switch { HaiType.Wan => 1, HaiType.Pin => 2, HaiType.Sou => 4, _ => 0 } << ((shuntsu.MinNumber - 1) * 3);
            }
            return Enumerable.Range(0, 7).Any(x => ((mask >> (x * 3)) & 7) == 7);
        }
    }

}
