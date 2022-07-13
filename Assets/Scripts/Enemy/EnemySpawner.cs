using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyInfo initEnemyInfo;
        public GameObject Enemy;
        public GameObject EnemyTemp;
        EnemyType SpawnEnemyType;
        public float MinX, MaxX, SpawnX; //Enemy스폰 위치 최소 최대 랜덤값
        private RoundManager RoundManager;

        public void Start()
        {
            RoundManager = RoundManager.Inst;
        }
        public void EnemySet(int MaxSpawn,float SpawnTime,EnemyType SpawnEnemyType)
        {
            switch (SpawnEnemyType)
            {
                case EnemyType.E100:
                    StartCoroutine(SpawnEnemyE100(MaxSpawn, SpawnTime));
                    break; 
                case EnemyType.E500:
                    StartCoroutine(SpawnEnemyE500(MaxSpawn, SpawnTime));
                    break;
                case EnemyType.E1000:
                    StartCoroutine(SpawnEnemyE1000(MaxSpawn, SpawnTime));
                    break;
                case EnemyType.E5000:
                    StartCoroutine(SpawnEnemyE5000(MaxSpawn, SpawnTime));
                    break;
                case EnemyType.E10000:
                    StartCoroutine(SpawnEnemyE10000(MaxSpawn, SpawnTime));
                    break;
            }
        }
        IEnumerator SpawnEnemyE100(int MaxSpawn,float SpawnTime)
        {
            int SpawnCount = 0;
            while (MaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                initEnemyInfo = new EnemyInfo(EnemyType.E100, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                SpawnCount++;
                //RoundManager.OnEnemyCreate(newEnemy);
            }
        }
        IEnumerator SpawnEnemyE500(int MaxSpawn,float SpawnTime)
        {
            int SpawnCount = 0;
            while (MaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                initEnemyInfo = new EnemyInfo(EnemyType.E500, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                SpawnCount++;
                //RoundManager.OnEnemyCreate(newEnemy);
            }
        }
        IEnumerator SpawnEnemyE1000(int MaxSpawn, float SpawnTime)
        {
            int SpawnCount = 0;
            while (MaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                initEnemyInfo = new EnemyInfo(EnemyType.E1000, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                SpawnCount++;
                //RoundManager.OnEnemyCreate(newEnemy);
            }
        }
        IEnumerator SpawnEnemyE5000(int MaxSpawn, float SpawnTime)
        {
            int SpawnCount = 0;
            while (MaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                initEnemyInfo = new EnemyInfo(EnemyType.E5000, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                SpawnCount++;
                //RoundManager.OnEnemyCreate(newEnemy);
            }
        }
        IEnumerator SpawnEnemyE10000(int MaxSpawn, float SpawnTime)
        {
            int SpawnCount = 0;
            while (MaxSpawn > SpawnCount)
            {
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                initEnemyInfo = new EnemyInfo(EnemyType.E10000, 100, 0.1f); //EnemyType, 체력, 속도 (추후 조정)
                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().InitEnemy(initEnemyInfo);
                SpawnCount++;
                //RoundManager.OnEnemyCreate(newEnemy);
            }
        }
    }
}