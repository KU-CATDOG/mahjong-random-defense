using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class EndNEndRelic : Relic
    {
        public override string Name => "EndNEnd";
        public override int MaxAmount => 10;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(critChance: towerStat.TowerInfo.Hais.Count(x => x.Spec.IsRoutou) * 1f);
        
    }
}