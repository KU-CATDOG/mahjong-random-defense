using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class StrongWindRelic : Relic
    {
        public override string Name => "StrongWind";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.C;
        public override Stat AdditionalStat(TowerStat towerStat) => new(critChance: towerStat.TowerInfo.Hais.Count(x => x.Spec.IsJangpung)*0.12f);
    }
}