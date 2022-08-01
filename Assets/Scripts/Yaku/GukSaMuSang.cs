using System.Linq;

namespace MRD
{
    public class GukSaMuSangChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "GukSaMuSang";
        public string[] OptionNames => new[] { nameof(GukSaMuSangStatOption), nameof(GukSaMuSangOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            bool[] checker = Enumerable.Repeat(false, 13).ToArray();
            int total = 0;
            foreach (var hai in holder.MentsuInfos.SelectMany(x => x.Hais))
            {
                int index = 0;
                total++;
                switch (hai.Spec.HaiType)
                {
                    case HaiType.Sou:
                        if (hai.Spec.Number is not (1 or 9)) return false;
                        index = hai.Spec.Number == 1 ? 0 : 1;
                        break;
                    case HaiType.Pin:
                        if (hai.Spec.Number is not (1 or 9)) return false;
                        index = hai.Spec.Number == 1 ? 2 : 3;
                        break;
                    case HaiType.Wan:
                        if (hai.Spec.Number is not (1 or 9)) return false;
                        index = hai.Spec.Number == 1 ? 4 : 5;
                        break;
                    case HaiType.Kaze:
                        index = 6 + hai.Spec.Number;
                        break;
                    case HaiType.Sangen:
                        index = 10 + hai.Spec.Number;
                        break;
                }

                checker[index] = true;
            }

            return total == 14 && checker.All(x => x);
        }
    }
}
