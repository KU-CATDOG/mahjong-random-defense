using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace MRD
{
    [RequireComponent(typeof(Grid))]
    public class RoundManager : Singleton<RoundManager>
    {
        public bool DEBUG_MODE;
        public RoundNum round; //{ get; private set; }
        public EnemySpawner Spawner => GetComponent<EnemySpawner>();
        public WaveController Wave => GetComponent<WaveController>();
        public Grid Grid => GetComponent<Grid>();
        public float playSpeed => gameSpeedMultiplier[gameSpeedMultiplierIndex] * gameSpeedOnOff;
        public int tsumoToken { get; private set; } = 0;
        public int playerHealth { get; private set; } = 25000;
        public Text roundText; // text 할당하기 화면 위 중앙에 있는것

        private float[] gameSpeedMultiplier = new float[3] {1f, 2f, 4f};
        private int gameSpeedMultiplierIndex = 0;
        private int gameSpeedOnOff = 0;

        [SerializeField]
        private CanvasComponents canvas;
        [SerializeField]
        private SpriteRenderer backgroundSprite;

        private void ResetSpeedButtons()
        {
            canvas.SpeedButtons[0].AddListenerOnly(() => gameSpeedMultiplierIndex = (gameSpeedMultiplierIndex + 1) % 3);
            canvas.SpeedButtons[1].AddListenerOnly(() => gameSpeedOnOff = 1 - gameSpeedOnOff);
        }

        private void ResetGame()
        {
            Grid.ResetGame();
            ResetSpeedButtons();
            NextRound();
        }

        private void InitGame()
        {
            Grid.InitGame();
            ResetGame();
        }

        private void Start()
        {
            
            if (!DEBUG_MODE)
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
            Spawner.EnemyList.Add(enemy);
        }

        public void OnEnemyDestroy(EnemyController enemy)
        {
            for (int i = Spawner.EnemyList.Count - 1; i >= 0; i--)
            {
                if (Spawner.EnemyList[i] != enemy) continue;

                Spawner.EnemyList.RemoveAt(i);
                Destroy(enemy.gameObject);
                return;
            }
        }

        public void PlayerDamage(int damage)
        {
            playerHealth -= damage;
        }

        public void NextRound()
        {
            if (!round.NextRound())
            {
                Wave.WaveStart(round.season*16+round.wind*4+round.number);
            }
            string seasonText = " ", windText = " ";
            switch (round.season)
            {
                case 0:
                    seasonText = "봄";
                    backgroundSprite.sprite = Resources.Load<Sprite>("Background/spring");
                    break;
                case 1:
                    seasonText = "여름";
                    backgroundSprite.sprite = Resources.Load<Sprite>("Background/summer");
                    break;
                case 2:
                    seasonText = "가을";
                    backgroundSprite.sprite = Resources.Load<Sprite>("Background/autumn");
                    break;
                case 3:
                    seasonText = "겨울";
                    backgroundSprite.sprite = Resources.Load<Sprite>("Background/winter");
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
            roundText.text = seasonText + "/" + windText + (round.number+1) + "국";
            round.NumberPlus();
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

        public void NumberPlus()
        {
            number++;
        }
    }
}