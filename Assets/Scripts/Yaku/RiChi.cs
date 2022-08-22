namespace MRD
{
    public class RiChiChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "RiChi";
        public string[] OptionNames => new[] { nameof(RiChiStatOption), nameof(RiChiImageOption) };

        public bool CheckCondition(YakuHolderInfo holder) => holder is CompleteTowerInfo cInfo && cInfo.RichiCount>0;
    }
}
