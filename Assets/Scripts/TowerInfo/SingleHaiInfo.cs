using System.Collections.Generic;

namespace MRD
{
    public class SingleHaiInfo : TowerInfo
    {
        public override AttackImage DefaultAttackImage => Hai.Spec.HaiType switch {
            HaiType.Sou => AttackImage.Sou,
            HaiType.Pin => AttackImage.Pin,
            HaiType.Wan => AttackImage.Wan,
            _ => AttackImage.Default,
        };
        public Hai Hai => hais[0];

        public SingleHaiInfo(Hai hai)
        {
            hais.Add(hai);
        }
    }
}
