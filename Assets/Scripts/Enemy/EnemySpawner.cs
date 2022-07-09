using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject Enemy;
        public int SpawnCount = 0, MaxSpawn; // 스폰횟수 카운팅, 몇번 스폰할지
        public float SpawnTime; // 소환 딜레이
        public float MinX, MaxX, SpawnX; //Enemy스폰 위치 최소 최대 랜덤값
        void Start()
        {
            StartCoroutine(SpawnEnemy());
        }
        void Update()
        {

        }
        IEnumerator SpawnEnemy()
        {
            while (MaxSpawn > SpawnCount)
            {
                Debug.Log("0");
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);
                Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                SpawnCount++;
                Debug.Log("3");
            }
        }
    }
}