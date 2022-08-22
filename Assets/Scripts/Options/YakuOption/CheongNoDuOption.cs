using System.Collections.Generic;

namespace MRD
{
    public class CheongNoDuStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongNoDuStatOption);

        public override Stat AdditionalStat => new
    (
            damageMultiplier: 2f,
            critChance: 0.25f,
            critDamage: 3f
    );
    }

    public class CheongNoDuOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongNoDuOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class CheongNoDuImageOption : TowerImageOption
    {
        public override string Name => nameof(CheongNoDuImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (45, 6) };
    }
}
