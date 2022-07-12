using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
 
    public class WaveController : MonoBehaviour
    {
        public int TestWaveNumber;
        public GameObject SpawnManager;
        void Start()
        {
            WaveStart(TestWaveNumber);
        }
        public void WaveStart(int wave)
        {
            switch (wave)
            {
                case 1:
                    SpawnManager.GetComponent<EnemySpawner>().EnemySet(8, 0.5f, EnemyType.E100);
                    break;
                case 2:
                    //   SpawnManager.GetComponent<EnemySpawner>().EnemySet(8,0.5f,EnemyType.E100);
                    break; 
                case 3:
                   // SpawnManager.GetComponent<EnemySpawner>().EnemySet(10,0.5f,EnemyType.E100);
                    break;                
                case 4:
                    SpawnManager.GetComponent<EnemySpawner>().EnemySet(5,0.5f,EnemyType.E100);
                    SpawnManager.GetComponent<EnemySpawner>().EnemySet(3,5f,EnemyType.E500);
                    break;
            }
        }

    }
}
