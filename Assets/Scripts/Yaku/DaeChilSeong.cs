namespace MRD
{
    public class DaeChilSeongChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "DaeChilSeong";
        public string[] OptionNames => new[] { nameof(DaeChilSeongStatOption), nameof(DaeChilSeongOption), nameof(DaeChilSeongImageOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            if (holder.MentsuInfos.Count != 7) return false;
            bool[] checker = new bool[7] { false, false, false, false, false, false, false }; // 동서남북백발중
            foreach (var it in holder.MentsuInfos)
            {
                if (it is not ToitsuInfo) return false;

                int index = it.Hais[0].Spec.Number;
                if (it.Hais[0].Spec.HaiType != HaiType.Sangen)
                {
                    index += 4;
                }
                else if (it.Hais[0].Spec.HaiType == HaiType.Kaze)
                {
                }
                else
                {
                    return false;
                }

                if (checker[index]) return false;
                checker[index] = true;
            }

            return true;
        }
    }
}
