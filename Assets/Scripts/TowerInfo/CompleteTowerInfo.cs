using System.Collections.Generic;

namespace MRD
{
    public class CompleteTowerInfo : MentsuInfo
    {
        private readonly List<MentsuInfo> mentsus = new List<MentsuInfo>();

        public IReadOnlyList<MentsuInfo> Mentsus => mentsus;

        public CompleteTowerInfo(TripleTowerInfo m1, MentsuInfo m2, MentsuInfo m3)
        {
            mentsus.AddRange(m1.Mentsus);
            mentsus.Add(m2);
            mentsus.Add(m3);

            hais.AddRange(m1.Hais);
            hais.AddRange(m2.Hais);
            hais.AddRange(m3.Hais);
        }
    }
}
