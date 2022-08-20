using System.Linq;
namespace MRD
{
    public class PingHuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "PingHu";
        public string[] OptionNames => new[] { nameof(PingHuImageOption), nameof(PingHuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
            => holder.MentsuInfos.All(x => x.IsMenzen && (x is ShuntsuInfo || !(x is ToitsuInfo && x.Hais.All(y => y.Spec.HaiType != HaiType.Sangen || y.Spec.HaiType == HaiType.Kaze && y.Spec.Number == RoundManager.Inst.round.wind))));

    }
}
