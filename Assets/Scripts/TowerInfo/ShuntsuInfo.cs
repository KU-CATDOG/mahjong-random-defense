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
            hais.Add(hai1);
            hais.Add(hai2);
            hais.Add(hai3);

            HaiType = hai1.Spec.HaiType;
            MinNumber = Mathf.Min(hai1.Spec.Number,hai2.Spec.Number,hai3.Spec.Number);
        }
        
        public bool Equals(ShuntsuInfo other) { // 최소 숫자와 타입이 같으면 같은 패
            return MinNumber == other.MinNumber &&
                   HaiType   == other.HaiType;
        }
    }
}
