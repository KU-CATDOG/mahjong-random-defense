using System;
using System.Collections.Generic;

namespace MRD
{
    public abstract class TowerOption : IEquatable<TowerOption>
    {
        public TowerStat HolderStat { get; private set; }

        public TowerInfo HolderInfo { get; private set; }

        public abstract string Name { get; }

        public void AttachOption(TowerStat holderStat)
        {
            HolderStat = holderStat;
            HolderInfo = HolderStat.Holder.TowerInfo;
            OnAttachOption();
        }

        protected virtual void OnAttachOption()
        {

        }

        /// <summary>
        /// 타워 파괴될 때 같이 불릴 함수
        /// </summary>
        public virtual void Dispose()
        {

        }

        public bool Equals(TowerOption other)
        {
            if (other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TowerOption);
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
