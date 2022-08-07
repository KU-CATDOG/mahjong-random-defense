using System.Linq;

namespace MRD
{
    public class DoraStatOption : TowerStatOption
    {
        public override string Name => nameof(DoraStatOption);

        public override Stat AdditionalStat => new
    (
            damagePercent: HolderStat.TowerInfo.Hais.SelectMany(x => x.DoraInfo.Values).Sum() * 20
    );
    }
}
