using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class RowColorRelic : Relic
    {
        public override string Name => "RowColor";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat)
        {
            HaiType oneType = towerStat.TowerInfo.Hais[0].Spec.HaiType;
            if(oneType is HaiType.Kaze or HaiType.Sangen) return new(damagePercent: 0f);
            int Y = towerStat.AttachedTower.Coordinate.Y;
            Grid grid = RoundManager.Inst.Grid;
            
            for(int i=0;i<5;i++){
                if(towerStat.AttachedTower.Coordinate.X == i) continue;
                foreach(var hai in grid.GetCell(new(i,Y)).TowerStat.TowerInfo.Hais)
                    if(hai.Spec.HaiType != oneType) 
                        return new(damagePercent: 0f);
            }

            return new(damagePercent: 0.5f);
        }
    }
}