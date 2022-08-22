using System.Collections.Generic;

namespace MRD
{
    public class RiChiStatOption : TowerStatOption
    {
        private int oneShotCount => (HolderStat.TowerInfo is CompleteTowerInfo cti && cti.RichiCount > 1) ? RoundManager.Inst.RelicManager[typeof(OneShotRelic)] : 0;
        public override string Name => nameof(RiChiStatOption);
        public override Stat AdditionalStat => new Stat
        (
            damageConstant: 20f * (1+oneShotCount),
            attackSpeed: 1.2f * (1+oneShotCount),
            critChance: .2f * (1+oneShotCount),
            critDamage: .3f * (1+oneShotCount)
        );
    }
    public class RiChiImageOption : TowerImageOption
    {
        public override string Name => nameof(RiChiImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (30, 20) };
    }
}
