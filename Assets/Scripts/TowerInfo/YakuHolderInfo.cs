using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class YakuHolderInfo : TowerInfo
    {
        protected readonly List<MentsuInfo> mentsus = new();

        private List<Yaku> yakuList;

        public IReadOnlyList<Yaku> YakuList => yakuList;

        public IReadOnlyList<MentsuInfo> MentsuInfos => mentsus;

        public bool isMenzen => MentsuInfos.All(x => x.IsMenzen);

        public void UpdateYaku()
        {
            yakuList = YakuConditionChecker.Instance.GetYakuList(this);
        }
    }
}
