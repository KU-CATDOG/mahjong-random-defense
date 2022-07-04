namespace MRD
{
    public class KantsuInfo : MentsuInfo
    {
        public KantsuInfo(Hai hai1, Hai hai2, Hai hai3, Hai hai4)
        {
            // 당연히 네 개가 같아야 함.
            hais.Add(hai1);
            hais.Add(hai2);
            hais.Add(hai3);
            hais.Add(hai4);
        }

        public override string ToString() => $"Kantsu : {hais[0]}";
    }
}
