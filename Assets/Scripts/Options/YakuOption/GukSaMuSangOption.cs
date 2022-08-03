using System.Collections.Generic;

namespace MRD
{
    public class GukSaMuSangStatOption : TowerStatOption
    {
        public override string Name => nameof(GukSaMuSangStatOption);
    }

    public class GukSaMuSangOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(GukSaMuSangOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
    public class GukSaMuSangImageOption : TowerImageOption
    {
        public override string Name => nameof(GukSaMuSangImageOption);
        protected override List<(int index, int order)> completeTowerImages => new() { (50, 1) };
    }
}
