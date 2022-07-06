using System.Linq;

namespace MRD
{
    public class SamSaekDongGakChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamSaekDongGak";

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int num = 0;
            foreach (var p in holder.MentsuInfos.SelectMany(x => x.Hais))
            {
                if (num == 0) num = p.Spec.Number;
                else
                    if (num != p.Spec.Number) return false;
            }
            return holder.MentsuInfos.Select(x => x.Hais).All(x => x is ShuntsuInfo or KantsuInfo);
        }
    }

}
