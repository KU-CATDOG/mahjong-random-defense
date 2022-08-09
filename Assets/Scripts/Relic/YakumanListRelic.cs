using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class YakumanListRelic : Relic
    {
        public override string Name => "YakumanList";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat)
        {
            var coord = towerStat.AttachedTower.Coordinate;
            int[,] eightWay = {{1,1,1,0,0,-1,-1,-1},
                               {1,0,-1,1,-1,1,0,-1}};
            var grid = RoundManager.Inst.Grid;
            float multiplier = 0f;
            for(int i=0;i<8;i++){
                if((coord.X+eightWay[0,i])<0 || 4<(coord.X+eightWay[0,i]) || (coord.Y+eightWay[1,i])<0 || 4<(coord.Y+eightWay[1,i])) continue;
                if(grid.GetCell(new(coord.X+eightWay[0,i],coord.Y+eightWay[1,i])).TowerStat.TowerInfo is YakuHolderInfo h)
                    multiplier += h.YakuList[0].IsYakuman? 0.5f : 0f;
            }
            return new(damageMultiplier: 1f+multiplier);
        }

    }
}