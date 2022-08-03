using System.Linq;

namespace MRD
{
    public class CheongNoDuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongNoDu";
        public string[] OptionNames => new[] { nameof(CheongNoDuStatOption), nameof(CheongNoDuOption), nameof(CheongNoDuImageOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            return holder.MentsuInfos.All(x => x.Hais.All(y => y.Spec.IsRoutou));
        }
    }
}
