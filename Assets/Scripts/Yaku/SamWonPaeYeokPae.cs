using System.Linq;

namespace MRD
{
    public class SamWonPaeYeokPaeChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "SamWonPaeYeokPae";

        public bool CheckCondition(YakuHolderInfo holder)
        {   //����� ���� : ���� �� �ϳ��� ����� Ŀ��
            return holder.MentsuInfos.Where(x => x is KoutsuInfo or KantsuInfo).
                Any(x => x.Hais[0].Spec.HaiType == HaiType.Sangen);
        }
    }
}
