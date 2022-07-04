using System.Linq;

namespace MRD
{
    public abstract class MentsuInfo : TowerInfo
    {
        public bool IsMenzen => hais.All(x => !x.IsFuroHai);
    }
}
