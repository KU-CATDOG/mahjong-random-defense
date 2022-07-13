using System;

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
        /// 특정 조건에서만 작동해야 하는 옵션들은 여기서 valid 여부 체크 (기획에 평화전도사 같은거)
        /// </summary>
        public virtual bool CheckCondition()
        {
            return true;
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
