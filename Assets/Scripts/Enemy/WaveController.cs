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
            SpawnManager.EnemySet((4*waveTemp), 2.5f, EnemyType.E100,(int)(100*(waveTemp * 0.2f)));
            SpawnManager.EnemySet((waveTemp/4)*3, 3f, EnemyType.E500,(int)(500 * (waveTemp * 0.2f)));
            SpawnManager.EnemySet((waveTemp/8)*2, 5f, EnemyType.E1000,(int)(1000 * (waveTemp * 0.1f)));
            SpawnManager.EnemySet((waveTemp/12), 8f, EnemyType.E5000,(int)(5000 * (waveTemp * 0.1f)));
            if(waveTemp%16 == 0)
                SpawnManager.EnemySet(waveTemp/16, 8f, EnemyType.E10000,(int)(10000 * (waveTemp * 0.1f)));
         //   Debug.Log(waveTemp);
          //  Debug.Log((int)(100 * (waveTemp * 0.2f)) + "," + (int)(500 * (waveTemp * 0.2f)) + "," + (int)(1000 * (waveTemp * 0.2f)) + "," + (int)(5000 * (waveTemp * 0.2f)) + ",");
        }

    }
}
    