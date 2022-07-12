using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject Enemy;
        public int SpawnCount = 0, MaxSpawn; // ����Ƚ�� ī����, ��� ��������
        public float SpawnTime; // ��ȯ ������
        public float MinX, MaxX, SpawnX; //Enemy���� ��ġ �ּ� �ִ� ������
        private RoundManager RoundManager;
        public EnemySpawner(RoundManager manager) {
            RoundManager = manager;
        }

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
                //Debug.Log("0");
                yield return new WaitForSeconds(SpawnTime);
                SpawnX = Random.Range(MinX, MaxX);

                var newEnemy = Instantiate(Enemy, new Vector3(SpawnX, 17f, 0f), Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().RoundManager = RoundManager;
                SpawnCount++;
                RoundManager.OnEnemyCreate(newEnemy);
                //Debug.Log("3");
            }
        }
    }
}