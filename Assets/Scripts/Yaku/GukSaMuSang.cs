using System.Linq;

namespace MRD
{
    public class GukSaMuSangChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "GukSaMuSang";
        public string[] OptionNames => new[] { nameof(GukSaMuSangStatOption), nameof(GukSaMuSangOption), nameof(GukSaMuSangImageOption) };

        public bool CheckCondition(YakuHolderInfo holder) => holder is KokushiTowerInfo;
    }
}
