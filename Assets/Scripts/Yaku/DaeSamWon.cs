using System.Linq;

namespace MRD
{
    public class DaeSamWonChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "DaeSamWon";
        public string[] OptionNames => new string[] { nameof(DaeSamWonStatOption),nameof(DaeSamWonOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {
            //조건: 삼원 3개 전부를 몸통으로
            return holder.MentsuInfos.Count(x => (x is KoutsuInfo or KantsuInfo) && x.Hais[0].Spec.HaiType == HaiType.Sangen) == 3; // 몸통 3개
        }
    }

}
