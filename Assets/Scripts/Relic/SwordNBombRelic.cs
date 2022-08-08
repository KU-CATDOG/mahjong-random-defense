using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class SwordNBombRelic : Relic
    {
        public override string Name => "SwordNBomb";
        public override int MaxAmount => 5;
        public override RelicRank Rank => RelicRank.C;
        public override void OnBuyAction() => RoundManager.Inst.GlobalRelicStat.BladeNExplosionSize += 0.1f;

    }
}