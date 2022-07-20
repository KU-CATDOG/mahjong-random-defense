using System.Linq;
using System.Collections.Generic;

namespace MRD
{
    public abstract class MentsuInfo : TowerInfo
    {
        public override AttackImage DefaultAttackImage => Hais[0].Spec.HaiType switch {
            HaiType.Sou => AttackImage.Sou,
            HaiType.Pin => AttackImage.Pin,
            HaiType.Wan => AttackImage.Wan,
            _ => AttackImage.Default,
        };
        public bool IsMenzen => hais.All(x => !x.IsFuroHai);
    }
}
