using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{ // FIXME: UNTESTED
    public class BlackSmithRelic : Relic
    {
        public override string Name => "BlackSmith";
        public override int MaxAmount => 5;
        public override RelicRank Rank => RelicRank.B;
        public override Stat AdditionalStat(TowerStat towerStat) {
            var count = RoundManager.Inst.Grid.GetYakuCount(new List<string>{nameof(ChanTaStatOption), nameof(JunJJangStatOption), nameof(HonNoDuStatOption)});
            float incrementRate = count[0] * 0.05f + count[1] * 0.1f + count[2] * 0.2f;
            return new(critDamage: incrementRate, critChance: towerStat.TowerInfo is CompleteTowerInfo ? incrementRate/10f : 0f);
        }
        
    }
}