using System.Collections.Generic;

namespace MRD
{
    public class RyangPeKoStatOption : TowerStatOption
    {
        public override string Name => nameof(RyangPeKoStatOption);

        public override float AdditionalAttack => 80.0f;

        public override int MaxRagePoint => 10000;

        public override float RageAttackPercent => 0.03f;

        public override float RageAttackSpeedMultiplier => 0.0002f;

        public override float RageCritChance => 0.01f;

        public override float RageCritMultiplier => 0.05f;
    }

    public class RyangPeKoOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(RyangPeKoOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
        }
    }
}
