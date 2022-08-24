using UnityEngine;

namespace MRD
{
    public class WaveController : MonoBehaviour
    {
        public float WanLootProb;
        public EnemySpawner SpawnManager => GetComponent<EnemySpawner>();

        private void Start()
        {
        }

        public void WaveStart(int wave)
        {
            int enemyNum = 0;
            int waveTemp = wave + 1;
            int season = wave / 16;
            float healthRatio = 1 + RoundManager.Inst.EnemyHealthAdder * Mathf.Pow(wave, RoundManager.Inst.EnemyHealthPower) + RoundManager.Inst.SeasonEnemyHealthAdder * Mathf.Pow(season, RoundManager.Inst.SeasonEnemyHealthPower);
            SpawnManager.InitWaveCount();
            SpawnManager.EnemySet(2*waveTemp, 0.5f, EnemyType.E100, Mathf.FloorToInt(150 * healthRatio), 1.5f);
            SpawnManager.EnemySet(waveTemp/4, 2f, EnemyType.E500, Mathf.FloorToInt(300 * healthRatio), 1.2f);
            SpawnManager.EnemySet(waveTemp/8, 3f, EnemyType.E1000, Mathf.FloorToInt(800 * healthRatio), 0.9f);
            SpawnManager.EnemySet(waveTemp/12, 4f, EnemyType.E5000, Mathf.FloorToInt(4000 * healthRatio), 0.5f);
            enemyNum = 2 * waveTemp + waveTemp / 4 + waveTemp / 8 + waveTemp / 12;

            if (waveTemp % 16 == 0
                || waveTemp > 16 && waveTemp % 8 == 0
                || waveTemp > 32 && waveTemp % 4 == 0
                || waveTemp > 48 && waveTemp % 2 == 0)
            {
                SpawnManager.EnemySet(waveTemp / 16, 8f, EnemyType.E10000, Mathf.FloorToInt(10000 * healthRatio), 0.3f);
                enemyNum++;
            }

            WanLootProb = Mathf.Pow(enemyNum, -0.5f);
        }
    }
}
