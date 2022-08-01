using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public abstract class MentsuInfo : TowerInfo
    {
        public override IReadOnlyList<string> DefaultOptions { get; } =
            new[] { nameof(MadiTowerOption), nameof(MadiTowerStatOption) };

        public override AttackImage DefaultAttackImage => Hais[0].Spec.HaiType switch
        {
            HaiType.Sou => AttackImage.Sou,
            HaiType.Pin => AttackImage.Pin,
            HaiType.Wan => AttackImage.Wan,
            _ => AttackImage.Default,
        };

        public bool IsMenzen => hais.All(x => !x.IsFuroHai);
    }
}
