namespace MRD
{
    public class PingHuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "PingHu";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {
            foreach(var m in holder.MentsuInfos)
            {
                if (!m.IsMenzen) return false;
                else
                {
                    if (m is ShuntsuInfo) continue;
                    else if (m is ToitsuInfo)
                    {
                        if (m.Hais[0].Spec.Number == RoundManager.Inst.round.wind || m.Hais[0].Spec.HaiType == HaiType.Sangen) return false;
                        else continue;
                    }
                    else return false;
                }
            }

            return true;
        }
    }

}
