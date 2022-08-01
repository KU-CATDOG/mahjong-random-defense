using System.Collections.Generic;

namespace MRD
{
    public class TangYaoStatOption : TowerStatOption
    {
        public override string Name => nameof(TangYaoStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 20.0f;
        public override float AdditionalAttackMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 1.2f : 1.0f;
    }

    public class TangYaoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(TangYaoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }

    public class TangYaoImageOption : TowerImageOption
    {
        public override string Name => nameof(TangYaoImageOption);

        protected override List<(int index, int order)> tripleTowerImages => new() { (23, 1) };
    }
}
