using System.Linq;

namespace MRD
{
    public class DoraStatOption : TowerStatOption
    {
        public override string Name => nameof(DoraStatOption);

        public override Stat AdditionalStat => new
        (
            damagePercent: RoundManager.Inst.Grid.doraList.GetDoraList.Select(x => HolderStat.TowerInfo.Hais.Count(y => x.Equals(y.Spec))).Sum() 
                * 0.05f * (1 + RoundManager.Inst.Grid.GetYakuCount(nameof(ShuKantSuStatOption)))
        );
    }
}
