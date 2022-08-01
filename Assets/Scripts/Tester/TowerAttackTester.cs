using UnityEngine;

namespace MRD
{
    public class TowerAttackTester : MonoBehaviour
    {
        public Tower tower;
        public Bullet bullet;

        public EnemyController enemy;

        // Start is called before the first frame update
        private void Start()
        {
            tower.TempInit();
        }
    }
}
