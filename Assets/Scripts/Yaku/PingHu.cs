using System.Linq;

namespace MRD
{
    public class PingHuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "PingHu";

        public bool CheckCondition(YakuHolderInfo holder)
        {
            foreach(var m in holder.MentsuInfos)
            {
                if (m is ShuntsuInfo) continue;
                else if (m is ToitsuInfo)
                {
                    if (m.Hais[0].Spec.Number == RoundManager.Inst.round.wind) return false;
                    else continue;
                }
                else return false;
            }

            return true;

            //return holder.MentsuInfos.All(x => x is ShuntsuInfo);
        }
    }

}
