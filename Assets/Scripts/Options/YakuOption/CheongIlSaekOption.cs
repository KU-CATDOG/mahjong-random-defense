using System.Collections.Generic;

namespace MRD
{
    public class CheongIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 20.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;

    }
    public class CheongIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            
        }
    }
    public class CheongIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(CheongIlSaekImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get => HolderStat.TowerInfo.Hais[0].Spec.HaiType switch
            {
                HaiType.Wan => new() { (3, 1) },
                HaiType.Pin => new() { (4, 1) },
                HaiType.Sou => new() { (5, 1) },
                _ => new() { },
            };

        }
    }
}
