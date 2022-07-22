using System.Linq;

namespace MRD
{
    public class CheongIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongIlSaek";
        public string[] OptionNames => new string[] { nameof(CheongIlSaekImageOption), nameof(CheongIlSaekStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   
            var group = holder.MentsuInfos.GroupBy(x => x.Hais[0].Spec.HaiType);
            return group.Count() == 1 && group.First().Key is HaiType.Wan or HaiType.Pin or HaiType.Sou;
        }
    }
}
