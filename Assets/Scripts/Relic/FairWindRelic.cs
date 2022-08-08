using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class FairWindRelic : Relic
    {
        public override string Name => "FairWind";
        public override int MaxAmount => 10;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(critDamage: towerStat.TowerInfo.Hais.Count(x => x.Spec.HaiType == HaiType.Kaze) * 0.05f);
    }
}