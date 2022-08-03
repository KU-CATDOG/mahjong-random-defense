using System.Linq;

namespace MRD
{
    public class GuRyeonBoDeungChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "GuRyeonBoDeung";
        public string[] OptionNames => new[] { nameof(GuRyeonBoDeungStatOption), nameof(GuRyeonBoDeungOption), nameof(GuRyeonBoDeungImageOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            if (holder.Hais.Count > 14 || !holder.isMenzen) return false;

            var type = holder.Hais[0].Spec.HaiType;
            if (type is HaiType.Kaze or HaiType.Sangen || holder.Hais.Any(x => x.Spec.HaiType != type)) return false;

            int[] counter = new int[] { 3, 1, 1, 1, 1, 1, 1, 1, 3 };
            foreach (var hai in holder.Hais) counter[hai.Spec.Number - 1]--;

            return counter.All(x => x < 1);
        }
    }
}
