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
                    RoundManager.PlusTsumoToken(4 + RoundManager.Inst.RelicManager[typeof(PensionRelic)]);
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

        public void BossSplit(int EnemyHealth, float EnemySpeed,Transform EnemyTransform)
        {
            var initEnemyInfo = new EnemyInfo(EnemyType.E10000, EnemyHealth, EnemySpeed);
            float EnemyLocationX = EnemyTransform.position.x;
            float EnemyLocationY = EnemyTransform.position.y;
            if (EnemyLocationX - 1.2 < 0)
            {
                var newEnemy = Instantiate(Enemy, new Vector3(1, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
                newEnemy = Instantiate(Enemy, new Vector3(3.4f, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
            }
            else if (EnemyLocationX + 1.2 > 9)
            {
                var newEnemy = Instantiate(Enemy, new Vector3(9, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
                newEnemy = Instantiate(Enemy, new Vector3(6.4f, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
            }
            else
            {
                var newEnemy = Instantiate(Enemy, new Vector3(EnemyLocationX-1.2f, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
                newEnemy = Instantiate(Enemy, new Vector3(EnemyLocationX + 1.2f, EnemyLocationY, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                RoundManager.OnEnemyCreate(newEnemy.GetComponent<EnemyController>());
            }



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
