using System.Collections.Generic;

namespace MRD
{
    public class HonIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(HonIlSaekStatOption);

        public override float AdditionalAttack => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 10.0f;
        public override float AdditionalAttackPercent => HolderStat.TowerInfo is CompleteTowerInfo ? 0.4f : 0.0f;
        public override float AdditionalAttackSpeedMultiplier => HolderStat.TowerInfo is CompleteTowerInfo ? 0.0f : 1.1f;


    }
    public class HonIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(HonIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {

        }
    }
    public class HonIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(HonIlSaekImageOption);

        protected override List<(int index, int order)> tripleTowerImages
        {
            get
            {
                int i = 0;
                int image = 0;
                var hais = HolderStat.TowerInfo.Hais;
                while (i >= 0)
                {
                    (image, i) = hais[i].Spec.HaiType switch
                    {
                        HaiType.Wan => (6, -1),
                        HaiType.Pin => (7, -1),
                        HaiType.Sou => (8, -1),
                        _ => (0, i + 1)
                    };
                }
                return new() { (image, 1) };
            }

        }
    }
}
