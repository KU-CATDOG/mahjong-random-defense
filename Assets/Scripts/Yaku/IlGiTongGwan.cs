using System.Linq;

namespace MRD
{
    public class IlGiTongGwanChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "IlGiTongGwan";

        public string[] OptionNames => new[]
            { nameof(IlGiTongGwanImageOption), nameof(IlGiTongGwanStatOption), nameof(IlGiTongGwanOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int mask = 0;
            foreach (var shift in holder.MentsuInfos
                .Where(x => x is ShuntsuInfo sh && sh.MinNumber % 3 == 1)
                .Cast<ShuntsuInfo>()
                .Select(x => 3 * ((int)x.HaiType / 10 - 1) + x.MinNumber / 3)) mask |= 1 << shift;
            return new int[] { 07, 070, 0700 }.Any(x => (mask & x) == x);
        }
    }
}
