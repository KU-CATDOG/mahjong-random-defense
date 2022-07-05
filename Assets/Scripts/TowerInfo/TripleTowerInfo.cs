using System.Collections.Generic;

namespace MRD
{
    public class TripleTowerInfo : MentsuInfo
    {
        private readonly List<MentsuInfo> mentsus = new List<MentsuInfo>();

        public IReadOnlyList<MentsuInfo> Mentsus => mentsus;

        public TripleTowerInfo(MentsuInfo m1, MentsuInfo m2, MentsuInfo m3)
        {
            mentsus.Add(m1);
            mentsus.Add(m2);
            mentsus.Add(m3);

            hais.AddRange(m1.Hais);
            hais.AddRange(m2.Hais);
            hais.AddRange(m3.Hais);
        }
    }
}
