using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MRD
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyInfo initEnemyInfo;
        public GameObject Enemy;
        public GameObject EnemyTemp;
        EnemyType SpawnEnemyType;
        public float MinX, MaxX; //Enemy스폰 위치 최소 최대 랜덤값
        private int NextRoundCheck = 0, WaveMaxSpawn = 0;
        private RoundManager RoundManager;
        public List<EnemyController> EnemyList = new(); // 현재 필드 위에 있는 적 리스트

        public void Start()
        {
            RoundManager = RoundManager.Inst;
        }
        public void Update()
        {
            if(NextRoundCheck != 0 && NextRoundCheck == WaveMaxSpawn)
            {
                if (!EnemyList.Any())
                {
                    RoundManager.NextRound();
                }

            }
                
        }
        public void InitWaveCount()// 초기화
        {
            NextRoundCheck = 0;
            WaveMaxSpawn = 0;
        }
        public void EnemySet(int TypeMaxSpawn,float SpawnTime,EnemyType SpawnEnemyType)
        {
            StartCoroutine(SpawnEnemy(TypeMaxSpawn, SpawnTime, SpawnEnemyType));
        }
        IEnumerator SpawnEnemy(int TypeMaxSpawn, float SpawnTime, EnemyType SpawnEnemyType)
        {
            float SpawnX = 0;
            int SpawnCount = 0;
            initEnemyInfo = new EnemyInfo(SpawnEnemyType, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)

            while (TypeMaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
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