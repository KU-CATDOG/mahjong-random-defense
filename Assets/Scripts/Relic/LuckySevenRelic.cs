using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class LuckySevenRelic : Relic
    {
        public override string Name => "LuckySeven";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat) 
            => new(damageMultiplier: towerStat.TowerInfo.Hais.Where(x=>!x.Spec.IsJi).Sum(x=>x.Spec.Number)==77 ? 4f : 1f);
    }
}