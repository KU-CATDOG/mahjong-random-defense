namespace MRD
{
    public class PingHuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "PingHu";
        public string[] OptionNames => new[] { nameof(PingHuImageOption), nameof(PingHuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            if (holder is not CompleteTowerInfo or TripleTowerInfo) return false;
            foreach (var m in holder.MentsuInfos)
            {
                if (!m.IsMenzen) return false;

                if (m is ShuntsuInfo) continue;

                if (m is ToitsuInfo)
                {
                    if (m.Hais[0].Spec.Number == RoundManager.Inst.round.wind ||
                        m.Hais[0].Spec.HaiType == HaiType.Sangen) return false;
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}
