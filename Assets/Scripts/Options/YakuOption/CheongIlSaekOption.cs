using System.Collections.Generic;

namespace MRD
{
    public class CheongIlSaekStatOption : TowerStatOption
    {
        public override string Name => nameof(CheongIlSaekStatOption);

        public override Stat AdditionalStat => new
    (
    );
    }

    public class CheongIlSaekOption : TowerProcessAttackInfoOption
    {
        public override string Name => nameof(CheongIlSaekOption);

        public override void ProcessAttackInfo(List<AttackInfo> infos)
        {
            var haiType = ((YakuHolderInfo)HolderStat.TowerInfo).MentsuInfos[0].Hais[0].Spec.HaiType;
            if (HolderStat.TowerInfo is not CompleteTowerInfo)
            {
                foreach (var info in infos)
                {
                    if (info is not BulletInfo bulletInfo) continue;
                    bulletInfo.UpdateShupaiLevel(haiType, 2);
                    // bulletInfo.SetImage(haiType,2);
                }
                return;
            }
            foreach (var info in infos)
            {
                if (info is not BulletInfo bulletInfo) continue;
                bulletInfo.UpdateShupaiLevel(haiType, 3);
            }
        }
    }

    public class CheongIlSaekImageOption : TowerImageOption
    {
        public override string Name => nameof(CheongIlSaekImageOption);

        protected override List<(int index, int order)> tripleTowerImages =>
            HolderStat.TowerInfo.Hais[0].Spec.HaiType switch
            {
                HaiType.Wan => new() { (3, 3) },
                HaiType.Sou => new() { (4, 3) },
                HaiType.Pin => new() { (5, 3) },
                _ => new() { },
            };
    }
}
