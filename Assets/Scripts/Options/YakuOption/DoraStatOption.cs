using System.Linq;

namespace MRD
{
    public class DoraStatOption : TowerStatOption
    {
        public override string Name => nameof(DoraStatOption);

        public override int AdditionalAttackPercent => HolderStat.TowerInfo.Hais.SelectMany(x => x.DoraInfo.Values).Sum() * 20;
    }
}