namespace MRD
{
    public class SingleTowerStatOption : TowerStatOption
    {
        private float additionalAttack;

        private float additionalCritChance;
        public override string Name => nameof(SingleTowerStatOption);
        public override Stat AdditionalStat => new
    (
            critChance: additionalCritChance,
            damageConstant: additionalAttack
    );
        protected override void OnAttachOption()
        {
            if (HolderStat.TowerInfo is not SingleHaiInfo singleHaiInfo) return;
            var haiSpec = singleHaiInfo.Hai.Spec;

            if (haiSpec.IsRoutou)
                additionalCritChance = 0.1f;
            else if (haiSpec.HaiType == HaiType.Sangen)
                additionalAttack = 5;
            else if (haiSpec.HaiType == HaiType.Kaze)
                additionalAttack = RoundManager.Inst.round.wind == haiSpec.Number ? 10 : 2;
        }
    }
}
