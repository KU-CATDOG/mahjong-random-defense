using System.Linq;

namespace MRD
{
    public class GuRyeonBoDeungChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "GuReonBoDeung";
        public string[] OptionNames => new string[] { nameof(GuRyeonBoDeungStatOption), nameof(GuRyeonBoDeungOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            HaiType type = holder.MentsuInfos[0].Hais[0].Spec.HaiType;
            int[] counter = Enumerable.Repeat<int>(0,9).ToArray();
            int total = 0;
            foreach(var hai in holder.MentsuInfos.SelectMany(x => x.Hais)){
                if(hai.Spec.HaiType != type) return false;
                counter[hai.Spec.Number - 1]++; total++;
            }

            if(total != 14) return false;
            for(int i=0;i<9;i++)
                if(i is 0 or 8) 
                    if(counter[i] is not (3 or 4))
                        return false;
                else 
                    if(counter[i] is not (1 or 2))
                        return false;
            return true;
        }
    }

}