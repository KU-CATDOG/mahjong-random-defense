namespace MRD
{
    public class HonIlSaekChecker : IYakuConditionChecker
    {
        public string TargetYakuName => "HonIlSaek";
        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder)
        {   //혼일색: 수패와 자패로 이루어지되, 모든 수패가 모양이 같아야 함
            HaiType? haiType = null; // 첫번째 수패 이전에는 null
            foreach (var it in holder.MentsuInfos)
            {
                if (!it.Hais[0].Spec.IsJi) { // 수패면
                    if(haiType == null) // 수패 최초 등장 시 기준 타입으로 지정
                        haiType = it.Hais[0].Spec.HaiType;
                    else
                        if(haiType != it.Hais[0].Spec.HaiType) // 두번째부터는 다르면 false 리턴
                            return false;
                }
            }
            return true;
        }
    }
}
