using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class SideSupportRelic : Relic
    {
        public override string Name => "SideSupport";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat) => new(attackSpeed: towerStat.AttachedTower.Coordinate.X is 1 or 5?1.1f:1f);
    }
}