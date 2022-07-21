using System.Linq;
using UnityEngine;
namespace MRD
{
    public class ShuntsuInfo : MentsuInfo
    {
        public int MinNumber { get; }
        public HaiType HaiType { get; }
        public ShuntsuInfo(Hai hai1, Hai hai2, Hai hai3)
        {
            // 당연히 세 개가 1씩 차이나야 함.
            var sort = new Hai[] { hai1, hai2, hai3 }.OrderBy(x => x.Spec.Number);
            hais.Add(sort.ElementAt(0));
            hais.Add(sort.ElementAt(1));
            hais.Add(sort.ElementAt(2));

            HaiType = hai1.Spec.HaiType;
            MinNumber = hais[0].Spec.Number;
        }
        
        public bool Equals(ShuntsuInfo other) { // 최소 숫자와 타입이 같으면 같은 패
            return MinNumber == other.MinNumber &&
                   HaiType   == other.HaiType;
        }
    }
}
