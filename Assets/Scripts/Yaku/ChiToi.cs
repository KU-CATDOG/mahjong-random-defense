namespace MRD
{
    public class ChiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ChiiToi";
        public string[] OptionNames => new string[] { nameof(ChiToiStatOption)};

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder is ChiToiTowerInfo;
        }
    }
}
