using System.Collections.Generic;
namespace MRD
{
    public class CompleteTowerInfo : YakuHolderInfo
    {
        public bool isRichi { get; }
        public CompleteTowerInfo(TripleTowerInfo m1, MentsuInfo m2, MentsuInfo m3)
        {
            mentsus.AddRange(m1.MentsuInfos);
            mentsus.Add(m2);
            mentsus.Add(m3);

            hais.AddRange(m1.Hais);
            hais.AddRange(m2.Hais);
            hais.AddRange(m3.Hais);
            
            isRichi = m1.RichiInfo is not null && m1.RichiInfo.State == RichiState.OnRichi;
        }
    }
}
