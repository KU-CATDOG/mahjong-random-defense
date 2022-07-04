namespace MRD
{
    public class ToitsuInfo : MentsuInfo
    {
        public ToitsuInfo(Hai hai1, Hai hai2)
        {
            // 당연히 두 개가 같아야 함.
            hais.Add(hai1);
            hais.Add(hai2);
        }
    }
}
