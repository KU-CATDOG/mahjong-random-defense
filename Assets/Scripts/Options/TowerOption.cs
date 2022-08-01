using System;

namespace MRD
{
    public abstract class TowerOption : IEquatable<TowerOption>
    {
        public TowerStat HolderStat { get; private set; }

        public abstract string Name { get; }

        public bool Equals(TowerOption other)
        {
            if (other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public void AttachOption(TowerStat holderStat)
        {
            HolderStat = holderStat;
            OnAttachOption();
        }

        protected virtual void OnAttachOption()
        {
        }

        /// <summary>
        ///     타워 파괴될 때 같이 불릴 함수
        /// </summary>
        public virtual void Dispose()
        {
        }

        public override bool Equals(object obj) => Equals(obj as TowerOption);

        public override int GetHashCode() => Name.GetHashCode();
    }
}
