namespace MRD
{
    public class ShuntsuInfo : MentsuInfo
    {
        public ShuntsuInfo(Hai hai1, Hai hai2, Hai hai3)
        {
            // 당연히 세 개가 1씩 차이나야 함.
            hais.Add(hai1);
            hais.Add(hai2);
            hais.Add(hai3);
        }
    }
}
