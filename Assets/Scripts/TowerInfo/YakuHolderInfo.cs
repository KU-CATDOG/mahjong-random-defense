using System.Collections.Generic;

namespace MRD
{
    public class YakuHolderInfo : TowerInfo
    {
        protected readonly List<MentsuInfo> mentsus = new();

        private List<Yaku> yakuList;

        public IReadOnlyList<Yaku> YakuList => yakuList;

        public IReadOnlyList<MentsuInfo> MentsuInfos => mentsus;

        public void UpdateYaku()
        {
            yakuList = YakuConditionChecker.Instance.GetYakuList(this);
        }
    }
}
