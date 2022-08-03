namespace MRD
{
    public class ChiToiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "ChiiToi";
        public string[] OptionNames => new[] { nameof(ChiToiStatOption), nameof(ChiToiOption), nameof(ChiToiImageOption) };

        public bool CheckCondition(YakuHolderInfo holder) => holder is ChiToiTowerInfo;
    }
}
