using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public abstract class TowerInfo : IEquatable<TowerInfo>
    {
        protected readonly List<Hai> hais = new();

        public IReadOnlyList<Hai> Hais => hais;

        public virtual IReadOnlyList<string> DefaultOptions { get; } = new string[] { };
        public virtual AttackImage DefaultAttackImage { get; set; } = AttackImage.Default;
        public int AttackCount { get; set; } = 0;
        public float TotalDamage { get; set; } = 0;

        public bool Equals(TowerInfo other)
        {
            if (other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TowerInfo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return hais.Aggregate(487, (current, item) => current * 31 + item.Id);
            }
        }

        public static bool operator ==(TowerInfo lh, TowerInfo rh)
        {
            if (lh is null && rh is null) return true;

            if (lh is null || rh is null) return false;

            return lh.Equals(rh);
        }

        public static bool operator !=(TowerInfo lh, TowerInfo rh)
        {
            return !(lh == rh);
        }
    }
}
