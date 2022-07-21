using System.Linq;

namespace MRD
{
    public class CheongNoDuChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "CheongNoDu";
        public string[] OptionNames => new string[] { nameof(CheongNoDuStatOption) };

        public bool CheckCondition(YakuHolderInfo holder)
        {   // ȥ���: ��� �а� �����(1 Ȥ�� 9)�θ� �̷����
            return holder.MentsuInfos.All(x => x.Hais.All(y => y.Spec.IsRoutou));// ��� �а� ������ �����ϴ��� Ȯ��
        }
    }
}
