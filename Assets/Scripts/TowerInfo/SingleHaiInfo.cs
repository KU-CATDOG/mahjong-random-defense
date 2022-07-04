namespace MRD
{
    public class SingleHaiInfo : TowerInfo
    {
        public Hai Hai => hais[0];

        public SingleHaiInfo(Hai hai)
        {
            hais.Add(hai);
        }
    }
}
