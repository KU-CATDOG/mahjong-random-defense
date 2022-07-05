namespace MRD
{
    public class TripleTowerInfo : YakuHolderInfo
    {
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
