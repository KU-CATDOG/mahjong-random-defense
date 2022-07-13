namespace MRD
{
    public abstract class TowerOption
    {
        public Tower Holder { get; private set; }

        public abstract string Name { get; }

        protected TowerOption(Tower holder)
        {
            Holder = holder;
        }

        /// <summary>
        /// 특정 조건에서만 작동해야 하는 옵션들은 여기서 valid 여부 체크 (기획에 평화전도사 같은거)
        /// </summary>
        public virtual bool CheckCondition()
        {
            return true;
        }
    }
}
