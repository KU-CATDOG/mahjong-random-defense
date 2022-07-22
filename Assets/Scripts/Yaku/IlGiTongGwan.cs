using System.Linq;

namespace MRD
{
    public class IlGiTongGwanChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "IlGiTongGwan";
        public string[] OptionNames => new string[] { nameof(IlGiTongGwanImageOption), nameof(IlGiTongGwanStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int hnum = 0;
            bool[] check = new bool[10];
            HaiType htype = holder.MentsuInfos[0].Hais[0].Spec.HaiType;

            for (int i = 1; i < 10; i++) check[i] = false;
            check[0] = true;

            if (holder.MentsuInfos.Any(x => x is ShuntsuInfo))
            {
                foreach(var p in holder.MentsuInfos.SelectMany(x => x.Hais))
                {
                    hnum = p.Spec.Number;

                    if (htype != p.Spec.HaiType) return false;

                    if (!check[hnum]) check[hnum] = true; //check 1~9
                    htype = p.Spec.HaiType;
                }
            }
            //else if(holder.MentsuInfos.Any(x => x is KoutsuInfo or KantsuInfo)) return false; //not Shuntsu

            foreach (bool t in check)
            {
                if (!t)
                    return false;
            }

            return true;
        }
    }

}
