namespace MRD
{
    public class EnemyInfo
    {
        public EnemyType enemyType { get; }
        public float initialHealth { get; }
        public float initialSpeed { get; }

    }
    public enum EnemyType
    {
        E100 = 100,
        E500 = 500,
        E1000 = 1000,
        E5000 = 5000,
        E10000 = 10000,
    }
}
