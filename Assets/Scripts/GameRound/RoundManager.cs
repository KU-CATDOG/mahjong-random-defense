using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace MRD
{
    [RequireComponent(typeof(Grid))]
    public class RoundManager : Singleton<RoundManager>
    {
        public bool DEBUG_MODE;
        public RoundNum round { get; private set; }
        public EnemySpawner Spawner => GetComponent<EnemySpawner>();
        public Grid Grid => GetComponent<Grid>();
        public float playSpeed { get; private set; }
        public int tsumoToken { get; private set; } = 0;
        public int playerHealth { get; private set; } = 25000;
        public Text roundText; // text 할당하기 화면 위 중앙에 있는것

        public List<EnemyController> EnemyList = new(); // 현재 필드 위에 있는 적 리스트

        private void ResetGame()
        {
            Grid.ResetGame();
        }

        private void InitGame()
        {
            playSpeed = 1f;
            Grid.InitGame();
            ResetGame();
        }

        private void Start()
        {
            if(!DEBUG_MODE)
                InitGame();
        }

        public void PlusTsumoToken(int GetToken)
        {
            tsumoToken += GetToken;
            return;
        }

        public bool MinusTsumoToken(int UseToken)
        {
            if (tsumoToken < UseToken)
            {
                return false;
            }
            else
            {
                tsumoToken -= UseToken;
                return true;
            }
        }

        public void OnEnemyCreate(EnemyController enemy)
        {
            EnemyList.Add(enemy);
        }

        public void OnEnemyDestroy(EnemyController enemy)
        {
            for (int i = EnemyList.Count - 1; i > 0; i--)
            {
                if (EnemyList[i] != enemy) continue;

                EnemyList.RemoveAt(i);
                return;
            }
        }

        public void PlayerDamage(int damage)
        {
            playerHealth -= damage;
        }

        public void NextRound()
        {
            round.NextRound();
            string seasonText = " ", windText = " ";
            switch (round.season)
            {
                case 0:
                    seasonText = "봄";
                    break;
                case 1:
                    seasonText = "여름";
                    break;
                case 2:
                    seasonText = "가을";
                    break;
                case 3:
                    seasonText = "겨울";
                    break;
            }
            switch (round.wind)
            {
                case 0:
                    windText = "동";
                    break;
                case 1:
                    windText = "남";
                    break;
                case 2:
                    windText = "서";
                    break;
                case 3:
                    windText = "북";
                    break;
            }
            roundText.text = seasonText + "/" + windText + round.number + "국";
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