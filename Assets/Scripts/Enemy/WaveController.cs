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
            int numRatio = Mathf.FloorToInt(Mathf.Pow(waveTemp / 4f, RoundManager.Inst.EnemyNumPower) + 1);
            int healthRatio = Mathf.FloorToInt(Mathf.Pow(waveTemp / 4f, RoundManager.Inst.EnemyHealthPower) + 1);
            SpawnManager.InitWaveCount();
            SpawnManager.EnemySet((2*waveTemp) * numRatio, 0.5f, EnemyType.E100,150 * healthRatio, 1.5f);
            SpawnManager.EnemySet((waveTemp/4)*3 * numRatio, 2f, EnemyType.E500,300 * healthRatio, 1.2f);
            SpawnManager.EnemySet((waveTemp/8)*2 * numRatio, 3f, EnemyType.E1000,800 * healthRatio, 0.9f);
            SpawnManager.EnemySet((waveTemp/12) * numRatio, 4f, EnemyType.E5000,4000 * healthRatio, 0.5f);
            if(waveTemp%16 == 0)
                SpawnManager.EnemySet(waveTemp/16, 8f, EnemyType.E10000,10000, 0.2f);
        }
    }
}
