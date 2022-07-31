using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public float playSpeed => gameSpeedMultiplier[gameSpeedMultiplierIndex];
        public int gameSpeedOnOff = 0;
        public int tsumoToken { get; private set; } = 0;
        public int playerHealth { get; private set; } = 25000;
        public Text roundText; // text 할당하기 화면 위 중앙에 있는것
        public Text tsumoTokenText; // 토큰갯수 나타내는 텍스트
        public Text healthText; // player채력 나타내는 텍스트
        private float[] gameSpeedMultiplier = new float[3] {1f, 2f, 4f};
        private int gameSpeedMultiplierIndex = 0;
        private bool gamePause = true; // false 게임 진행, true 게임 멈춤

        [SerializeField]
        private GameObject timer;

        [SerializeField]
        private CameraShake cs;

        [SerializeField]
        private CanvasComponents canvas;
        [SerializeField]
        private SpriteRenderer backgroundSprite;
        private Sprite[] backgroundSpriteArr;

        private void ResetSpeedButtons()
        {
            canvas.ChangeSpeedButtonImage(0, 0);
            canvas.ChangeSpeedButtonImage(1, 4);
            canvas.SpeedButtons[0].AddListenerOnly(() => 
            {
                gameSpeedMultiplierIndex = (gameSpeedMultiplierIndex + 1) % 3;
                canvas.ChangeSpeedButtonImage(0, gameSpeedMultiplierIndex);
            } );
            canvas.SpeedButtons[1].AddListenerOnly(() => 
            {
                gamePause = !gamePause;
                if (gamePause)
                {
                    canvas.ChangeSpeedButtonImage(1, 3);
                }
                else
                {
                    if (gameSpeedOnOff == 0)
                    {
                        canvas.ChangeSpeedButtonImage(1, 3);
                        gamePause = true;
                        gameSpeedOnOff = 1;
                    }
                    else
                    {
                        canvas.ChangeSpeedButtonImage(1, 5);
                    }
                }
            });
        }

        private void ResetGame()
        {
            Grid.ResetGame();
            ResetSpeedButtons();
            NextRound();
            tsumoToken = 5000;
            playerHealth = 25000;
            tsumoTokenText.text = ""+tsumoToken;
            healthText.text = "" + playerHealth;
        }

        private void InitGame()
        {
            Grid.InitGame();
            ResetGame();
        }

        private void Start()
        {
            backgroundSpriteArr = ResourceDictionary.GetAll<Sprite>("Background");
            if (!DEBUG_MODE)
                InitGame();
        }

        public void PlusTsumoToken(int GetToken)
        {
            tsumoToken += GetToken;
            tsumoTokenText.text = "" + tsumoToken;
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
                tsumoTokenText.text = "" + tsumoToken;
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
            healthText.text = "" + playerHealth;
            canvas.DamageOverlay.SetDamageOverlay(damage/1500f);
            if(playerHealth <= 0)
            {
                SceneManager.LoadScene("StartScene");
            }

            StartCoroutine(cs.Shake(0.3f, 0.005f));
        }

        public void NextRound()
        {
            if(gamePause)
            {
                gameSpeedOnOff = 0;
                canvas.ChangeSpeedButtonImage(1, 4);
            }
            if (!round.NextRound())
            {
                Wave.WaveStart(round.season*16+round.wind*4+round.number);
            }
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
            backgroundSprite.sprite = backgroundSpriteArr[round.season];
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
        public void AttachTimer(float targetTime, int targetCount, Tower coroutineOwner, Timer.OnTick onTick)
        {
            var newTimer = Instantiate(timer);
            newTimer.transform.SetParent(transform);
            newTimer.GetComponent<Timer>().Init(targetTime, targetCount, coroutineOwner, onTick);
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