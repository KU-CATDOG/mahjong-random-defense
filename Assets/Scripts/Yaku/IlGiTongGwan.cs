using System.Linq;

namespace MRD
{
    public class IlGiTongGwanChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "IlGiTongGwan";

        public bool CheckCondition(YakuHolderInfo holder)
        {
            int hnum = 0;
            bool[] check = new bool[9];

            for (int i = 0; i < 9; i++) check[i] = false;
            
            if (holder.MentsuInfos.Select(x => x.Hais).All(x => x is ShuntsuInfo))
            {
                foreach(var p in holder.MentsuInfos.SelectMany(x => x.Hais))
                {
                    hnum = p.Spec.Number;

                    if (!check[hnum - 1]) check[hnum - 1] = true; //check 1~9
                }

                foreach (bool t in check)
                {
                    if (!t) return false;
                } 
            }
            else return false; //not Shuntsu

            return true;
        }
    }

}
