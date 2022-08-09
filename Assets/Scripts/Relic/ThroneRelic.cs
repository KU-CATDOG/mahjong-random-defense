using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class ThroneRelic : Relic
    {
        public override string Name => "Throne";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.S;
        public override Stat AdditionalStat(TowerStat towerStat)
        {
            if(towerStat.AttachedTower.Coordinate.Equals(3,5)) return new(damageMultiplier: 2f,attackSpeed: 1.8f,critChance: 0.4f, critDamage: 1f);
            return new();
        }

    }
}