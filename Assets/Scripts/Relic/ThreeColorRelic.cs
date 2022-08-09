using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace MRD
{
    public class ThreeColorRelic : Relic
    {
        public override string Name => "ThreeColor";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;

        public override Stat AdditionalStat(TowerStat towerStat)
        {
            if(towerStat.Options.ContainsKey(nameof(SamSaekDongGakStatOption)) && towerStat.Options.ContainsKey(nameof(SamSaekDongSoonStatOption)))
                return new(damagePercent: RoundManager.Inst.Grid.GetYakuCount(new List<string>{nameof(SamSaekDongGakOption),nameof(SamSaekDongSoonOption)}).Sum()/3 * 0.5f);
            return new();
        }
    }
}