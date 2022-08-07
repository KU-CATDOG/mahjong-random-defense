using System.Collections.Generic;

namespace MRD
{
    public class DaeSaHeeStatOption : TowerStatOption
    {
        public override string Name => nameof(DaeSaHeeStatOption);
        public override Stat AdditionalStat => new Stat
        (
            attackSpeed: 0.2f,
            damagePercent: 0.25f
        );
        public override AttackBehaviour AttackBehaviour => new BladeAttackBehaviour(true);
    }

    public class DaeSaHeeOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(DaeSaHeeOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class DaeSaHeeImageOption : TowerImageOption
    {
        public override string Name => nameof(DaeSaHeeImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (35, 1), (37, 6) };
    }
}
