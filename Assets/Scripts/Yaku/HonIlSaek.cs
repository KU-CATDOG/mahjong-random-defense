using System.Linq;

namespace MRD
{
    public class HonIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "HonIlSaek";

        public string[] OptionNames => new[]
            { nameof(HonIlSaekImageOption), nameof(HonIlSaekStatOption), nameof(HonIlSaekOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            var group = holder.Hais.GroupBy(x => x.Spec.HaiType)
                .Where(g => g.Key is HaiType.Wan or HaiType.Pin or HaiType.Sou);
            return group.Count() == 1;
        }
    }
}
