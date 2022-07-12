using System.Linq;

namespace MRD
{
    public class JaIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "JaIlSaek";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {   //자일색: 몸통 자패 커쯔나 캉쯔 4개, 자패 머리 1개
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.IsJi) == 4 && // 몸통 4개
                    holder.MentsuInfos.Count(x => (x is ToitsuInfo) && x.Hais[0].Spec.IsJi) == 1; //머리 하나
        }
    }
}
