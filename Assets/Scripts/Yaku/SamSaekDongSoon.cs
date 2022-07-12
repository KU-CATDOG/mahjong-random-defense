using System.Linq;

namespace MRD
{
    public class SamSaekDongSoonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamSaekDongSoon";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int num = 0;
            bool[] check = new bool[3];

            for (int i = 0; i < 3; i++) check[i] = false;

            if (holder.MentsuInfos.Any(x => x is ShuntsuInfo))
            {
                foreach (var p in holder.MentsuInfos.Select(x => x.Hais))
                {
                    if (num == 0) num = p[0].Spec.Number;
                    else
                        if (num != p[0].Spec.Number) return false;

                    if (p[0].Spec.HaiType == HaiType.Wan) check[0] = true;
                    else if (p[0].Spec.HaiType == HaiType.Pin) check[1] = true;
                    else if (p[0].Spec.HaiType == HaiType.Sou) check[2] = true;
                    else return false;
                }
            }
            //else if (holder.MentsuInfos.Any(x => x is KoutsuInfo or KantsuInfo)) return false;

            foreach (bool t in check)
            {
                if (!t)
                    return false;
            }

            return true;
        }
    }

}
