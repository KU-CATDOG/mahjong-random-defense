using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class KokushiTowerInfo : YakuHolderInfo
    {
        public KokushiTowerInfo(List<SingleHaiInfo> singleHais)
        {
            hais.AddRange(singleHais.Select(x => x.Hai));
        }
    }
}
