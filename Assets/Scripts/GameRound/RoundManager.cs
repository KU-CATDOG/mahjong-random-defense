using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    [RequireComponent(typeof(Grid))]
    public class RoundManager : Singleton<RoundManager>
    {
        public bool DEBUG_MODE;
        public RoundNum round { get; private set; }
        private EnemySpawner spawner => GetComponent<EnemySpawner>();
        private Grid grid;
        public float playSpeed { get; private set; }
        public List<GameObject> EnemyList = new List<GameObject>(); // 현재 필드 위에 있는 적 리스트

        private void ResetGame()
        {
            grid.ResetGame();
        }

        private void InitGame()
        {
            playSpeed = 1f;
            grid = GetComponent<Grid>();
            grid.InitGame();
            ResetGame();
        }

        private void Start()
        {
            if(!DEBUG_MODE)
                InitGame();
        }

        public void OnEnemyCreate(GameObject enemy)
        {
            EnemyList.Add(enemy);
        }

        public void OnEnemyDestroy(GameObject enemy)
        {
            for(int i=0;i<EnemyList.Count;i++)
                if(EnemyList[i] == enemy) {
                    EnemyList.RemoveAt(i);
                    return;
                }
        }
    }

    public struct RoundNum
    {
        public int season { get; private set; }
        public int wind { get; private set; }
        public int number { get; private set; }

        // ���� ���� �� true
        public bool NextRound()
        {
            number++;
            if (number > 3)
            {
                number = 0;
                wind++;
            }
            if (wind > 3)
            {
                wind = 0;
                season++;
            }
            if (season > 3)
            {
                return true;
            }
            return false;
        }
    }
}