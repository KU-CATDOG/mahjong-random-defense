using System.Collections.Generic;

namespace MRD
{
    public class SoSamWonStatOption : TowerStatOption
    {
        public override string Name => nameof(SoSamWonStatOption);

        public override Stat AdditionalStat => new
    (
            damageMultiplier: HolderStat.TowerInfo is CompleteTowerInfo ? 1.0f : 2.0f,
            attackSpeed: HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 0.5f
    );
    }

    public class SoSamWonOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(SoSamWonOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }

    public class SoSamWonImageOption : TowerImageOption
    {
        public override string Name => nameof(SoSamWonImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (19, 12) };
    }
}
