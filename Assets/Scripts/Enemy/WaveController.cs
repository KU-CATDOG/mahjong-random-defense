using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
 
    public class WaveController : MonoBehaviour
    {
        public int TestWaveNumber;
        public EnemySpawner SpawnManager;
        void Start()
        {
            WaveStart(TestWaveNumber);
        }
        public void WaveStart(int wave)
        {
            switch (wave)
            {
                case 1:
                    SpawnManager.EnemySet(5,0.5f,EnemyType.E100);
                    break;
                case 2:
                    SpawnManager.EnemySet(8,0.5f,EnemyType.E100);
                    break; 
                case 3:
                    SpawnManager.EnemySet(10,0.5f,EnemyType.E100);
                    break;                
                case 4:
                    SpawnManager.EnemySet(5,0.5f,EnemyType.E100);
                    SpawnManager.EnemySet(3,0.8f,EnemyType.E500);
                    break;
            }
        }

    }
}
