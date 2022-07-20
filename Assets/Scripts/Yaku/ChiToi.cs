namespace MRD
{
    public class ChiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ChiiToi";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder is ChiToiTowerInfo;
        }
    }
}
