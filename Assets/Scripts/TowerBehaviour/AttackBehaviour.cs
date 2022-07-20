namespace MRD
{
    public abstract class AttackBehaviour
    {
        public Tower Tower { get; private set; }

        public void Init(Tower tower)
        {
            Tower = tower;
            OnInit();
        }

        public abstract void OnUpdate();
        public abstract void OnInit();
    }
}
