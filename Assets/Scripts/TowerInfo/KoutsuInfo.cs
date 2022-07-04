namespace MRD
{
    public class KoutsuInfo : MentsuInfo
    {
        public KoutsuInfo(Hai hai1, Hai hai2, Hai hai3)
        {
            // 당연히 세 개가 같아야 함.
            hais.Add(hai1);
            hais.Add(hai2);
            hais.Add(hai3);
        }
    }
}
