
using System;

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
    }

    public class Hai : IEquatable<Hai>
    {
        // 이거 패산 바뀌면 1000씩 증가하도록 해야 함 (서로 다른 패산의 패가 같은 패로 판별되는 것 방지)
        public int Id { get; }

        public HaiSpec Spec { get; }

        public bool IsFuroHai { get; }

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
