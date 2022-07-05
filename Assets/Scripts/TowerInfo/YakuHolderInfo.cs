using System.Collections.Generic;

namespace MRD
{
    public class YakuHolderInfo : TowerInfo
    {
        protected readonly List<MentsuInfo> mentsus = new();

        public IReadOnlyList<MentsuInfo> MentsuInfos => mentsus;
    }
}
