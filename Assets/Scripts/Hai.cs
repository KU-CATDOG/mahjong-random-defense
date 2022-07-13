
using System;
using System.Collections.Generic;

namespace MRD
{
    public enum HaiType
    {
        Wan = 10,
        Pin = 20,
        Sou = 30,
        Kaze = 40,
        Sangen = 50,
    }

    public class HaiSpec : IEquatable<HaiSpec>
    {
        public HaiType HaiType { get; }

        /// <summary>
        /// 풍패(Kaze) | 0:동 1:남 2:서 3:북 <br/>
        /// 삼원패(Sangen) | 0:백 1:발 2:중 <br/>
        /// 수패(Shupai) | 1~9
        /// </summary>
        public int Number { get; }

        public HaiSpec(HaiType haiType, int number)
        {
            HaiType = haiType;
            Number = number;
        }

        public bool Equals(HaiSpec other)
        {
            if (other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HaiSpec);
        }

        public override int GetHashCode()
        {
            return (int)HaiType + Number;
        }

        public static bool operator ==(HaiSpec lh, HaiSpec rh)
        {
            if (lh is null && rh is null) return true;

            if (lh is null || rh is null) return false;

            return lh.Equals(rh);
        }

        public static bool operator !=(HaiSpec lh, HaiSpec rh)
        {
            return !(lh == rh);
        }

        public bool IsJi => HaiType is HaiType.Sangen or HaiType.Kaze;
        public bool IsRoutou => Number is 1 or 9 && !IsJi;

        public bool IsYaochu => IsJi || IsRoutou;
    }

    public class Hai : IEquatable<Hai>
    {
        public int Id { get; }

        public HaiSpec Spec { get; }

        public bool IsFuroHai { get; }

        /// <summary>
        /// 이 패에 붙어있는 도라 종류. 아카도라 하나, 그냥 도라 둘 등등 딕셔너리에 저장하고 Values 합이 총 도라 수
        /// </summary>
        private readonly Dictionary<string, int> doraInfo = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> DoraInfo => doraInfo;

        public Hai(int id, HaiSpec spec)
        {
            Id = id;
            Spec = spec;
        }

        public bool Equals(Hai other)
        {
            if (other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Hai);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Hai lh, Hai rh)
        {
            if (lh is null && rh is null) return true;

            if (lh is null || rh is null) return false;

            return lh.Equals(rh);
        }

        public static bool operator !=(Hai lh, Hai rh)
        {
            return !(lh == rh);
        }
    }
}
