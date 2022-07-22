using UnityEngine;
namespace MRD
{
 
    public class WaveController : MonoBehaviour
    {
        public int testWaveNumber;
        public EnemySpawner SpawnManager => GetComponent<EnemySpawner>();
        void Start()
        {
        }
        public void WaveStart(int wave)
        {
            SpawnManager.InitWaveCount();
            switch (wave)
            {
                case 0:
                    SpawnManager.EnemySet(5,2.5f,EnemyType.E100);
                    break;
                case 1:
                    SpawnManager.EnemySet(8,2.5f,EnemyType.E100);
                    break; 
                case 2:
                    SpawnManager.EnemySet(10,2.5f,EnemyType.E100);
                    break;                
                case 3:
                    SpawnManager.EnemySet(5,2.5f,EnemyType.E100);
                    SpawnManager.EnemySet(3,3f,EnemyType.E500);
                    break;
            }
        }

    }
}
    