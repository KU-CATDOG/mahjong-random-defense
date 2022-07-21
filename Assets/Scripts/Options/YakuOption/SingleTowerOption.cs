using UnityEngine;
namespace MRD
{
    public class SingleTowerStatOption : TowerStatOption
    {
        public override string Name => nameof(SingleTowerStatOption);
        public override float AdditionalCritChance => additionalCritChance;
        public override float AdditionalAttack => additionalAttack;

        private float additionalCritChance = 0f;
        private float additionalAttack = 0f;
        protected override void OnAttachOption()
        {
            if(HolderStat.TowerInfo is not SingleHaiInfo singleHaiInfo) return;
            var haiSpec = singleHaiInfo.Hai.Spec;

            if(haiSpec.IsRoutou)
                additionalCritChance = 0.1f;
            else if(haiSpec.HaiType == HaiType.Sangen)
                additionalAttack = 5;
            else if(haiSpec.HaiType == HaiType.Kaze) 
                additionalAttack = (RoundManager.Inst.round.wind == haiSpec.Number) ? 10 : 2;
        }
    }
}