using UnityEngine;
namespace MRD
{
 
    public class WaveController : MonoBehaviour
    {
        public int testWaveNumber;
        private int waveTemp;
        public EnemySpawner SpawnManager => GetComponent<EnemySpawner>();
        void Start()
        {
        }
        public void WaveStart(int wave)
        {
            waveTemp = wave + 1;
            SpawnManager.InitWaveCount();
            SpawnManager.EnemySet((4*waveTemp), 0.5f, EnemyType.E100,100);
            SpawnManager.EnemySet((waveTemp/4)*3, 2f, EnemyType.E500,500);
            SpawnManager.EnemySet((waveTemp/8)*2, 3f, EnemyType.E1000,1000);
            SpawnManager.EnemySet((waveTemp/12), 4f, EnemyType.E5000,5000);
            if(waveTemp%16 == 0)
                SpawnManager.EnemySet(waveTemp/16, 8f, EnemyType.E10000,10000);
         //   Debug.Log(waveTemp);
          //  Debug.Log((int)(100 * (waveTemp * 0.2f)) + "," + (int)(500 * (waveTemp * 0.2f)) + "," + (int)(1000 * (waveTemp * 0.2f)) + "," + (int)(5000 * (waveTemp * 0.2f)) + ",");
        }

    }
}
    