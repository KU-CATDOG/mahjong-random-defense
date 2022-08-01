using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject Enemy;
        public GameObject EnemyTemp;
        public float MinX, MaxX; //Enemy스폰 위치 최소 최대 랜덤값
        public List<EnemyController> EnemyList = new(); // 현재 필드 위에 있는 적 리스트
        private int NextRoundCheck, WaveMaxSpawn;
        private RoundManager RoundManager;


        public void Start()
        {
            RoundManager = RoundManager.Inst;
        }

        public void Update()
        {
            if (NextRoundCheck != 0 && NextRoundCheck == WaveMaxSpawn)
                if (!EnemyList.Any())
                {
                    RoundManager.PlusTsumoToken(6);
                    RoundManager.NextRound();
                    RoundManager.Grid.DescentUpgrade();
                }
        }

        public void InitWaveCount() // 초기화
        {
            NextRoundCheck = 0;
            WaveMaxSpawn = 0;
        }

        public void EnemySet(int TypeMaxSpawn, float SpawnTime, EnemyType SpawnEnemyType, int EnemyHealth,
            float EnemySpeed)
        {
            if (TypeMaxSpawn != 0)
                StartCoroutine(SpawnEnemy(TypeMaxSpawn, SpawnTime, SpawnEnemyType, EnemyHealth, EnemySpeed));
        }

        private IEnumerator SpawnEnemy(int TypeMaxSpawn, float SpawnTime, EnemyType SpawnEnemyType, int EnemyHealth,
            float EnemySpeed)
        {
            float timer = 0;
            float nextDelay = 0;
            float SpawnX = 0;
            int SpawnCount = 0;
            var initEnemyInfo = new EnemyInfo(SpawnEnemyType, EnemyHealth, EnemySpeed); //EnemyType, 체력, 속도 (추후 조정)
            NextRoundCheck += TypeMaxSpawn;

            while (TypeMaxSpawn > SpawnCount)
            {
                timer = 0f;
                nextDelay = SpawnTime * Random.Range(.8f, 1.25f);
                while (timer < nextDelay)
                {
                    timer += Time.deltaTime * RoundManager.Inst.playSpeed * RoundManager.Inst.gameSpeedOnOff;
                    yield return null;
                }

                SpawnX = Random.Range(MinX, MaxX);
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
                SpawnCount++;
                WaveMaxSpawn++;
            }
        }
    }
}
