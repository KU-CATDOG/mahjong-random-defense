using System.Linq;
namespace MRD
{
    public class PingHuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "PingHu";
        public string[] OptionNames => new[] { nameof(PingHuImageOption), nameof(PingHuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
            => holder.MentsuInfos.All(x => x.IsMenzen && (x is ShuntsuInfo || (x is ToitsuInfo && !x.Hais[0].Spec.Equals(HaiType.Kaze, RoundManager.Inst.round.wind) && x.Hais[0].Spec.HaiType != HaiType.Sangen)));

    }
}
