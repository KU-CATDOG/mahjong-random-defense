
namespace MRD
{
    public class RowColorRelic : Relic
    {
        public override string Name => "RowColor";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;
        public override Stat AdditionalStat(TowerStat towerStat)
        {
            HaiType? oneType = null;
            int X = towerStat.AttachedTower.Coordinate.X;
            Grid grid = RoundManager.Inst.Grid;
            
            for(int i=0;i<5;i++){
                if(towerStat.AttachedTower.Coordinate.Y == i) continue;
                var info = grid.GetCell(new(X,i)).TowerStat.TowerInfo;
                if(info is null) continue;

                foreach(var hai in info.Hais)
                {
                    if(hai.Spec.IsJi) continue;
                    if(oneType is null) oneType = hai.Spec.HaiType;
                    if(hai.Spec.HaiType != oneType) 
                        return new(damagePercent: 0f);
                }
            }

            return new(damagePercent: 0.25f);
        }
    }
}