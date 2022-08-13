using System.Collections.Generic;
namespace MRD
{
    public class CompleteTowerInfo : YakuHolderInfo
    {
        private List<string> defaultOptions;
        public override IReadOnlyList<string> DefaultOptions => defaultOptions;
        public CompleteTowerInfo(TripleTowerInfo m1, MentsuInfo m2, MentsuInfo m3)
        {
            mentsus.AddRange(m1.MentsuInfos);
            mentsus.Add(m2);
            mentsus.Add(m3);

            hais.AddRange(m1.Hais);
            hais.AddRange(m2.Hais);
            hais.AddRange(m3.Hais);
            
            defaultOptions = m1.RichiInfo is not null && m1.RichiInfo.State == RichiState.OnRichi ?
                new List<string> { nameof(RiChiStatOption), nameof(RiChiImageOption) } 
                : new();
        }
    }
}
