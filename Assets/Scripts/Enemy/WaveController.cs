using UnityEngine;

namespace MRD
{
    public class WaveController : MonoBehaviour
    {
        public int testWaveNumber;
        private int waveTemp;
        public EnemySpawner SpawnManager => GetComponent<EnemySpawner>();

        private void Start()
        {
        }

        public void WaveStart(int wave)
        {
            waveTemp = wave + 1;
            SpawnManager.InitWaveCount();
            SpawnManager.EnemySet((2*waveTemp), 0.5f, EnemyType.E100,200,1.5f);
            SpawnManager.EnemySet((waveTemp/4)*3, 2f, EnemyType.E500,500,1.2f);
            SpawnManager.EnemySet((waveTemp/8)*2, 3f, EnemyType.E1000,1000,0.9f);
            SpawnManager.EnemySet((waveTemp/12), 4f, EnemyType.E5000,5000,0.5f);
            if(waveTemp%16 == 0)
                SpawnManager.EnemySet(waveTemp/16, 8f, EnemyType.E10000,10000, 0.2f);
        }
    }
}
