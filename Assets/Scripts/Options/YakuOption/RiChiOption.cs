using System.Collections.Generic;

namespace MRD
{
    public class RiChiStatOption : TowerStatOption
    {
        public override string Name => nameof(RiChiStatOption);
        public override Stat AdditionalStat => new Stat
        (
            damageConstant: 20f,
            attackSpeed: 1.2f,
            critChance: .2f,
            critDamage: .3f
        );
    }
    public class RiChiImageOption : TowerImageOption
    {
        public override string Name => nameof(RiChiImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (30, 20) };
    }
}
