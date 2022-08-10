using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MRD
{
    [RequireComponent(typeof(Grid))]
    public class RoundManager : Singleton<RoundManager>
    {
        public int gameSpeedOnOff;
        public int optionOnOff = 1;
        public Text roundText; // text 할당하기 화면 위 중앙에 있는것
        public Text tsumoTokenText; // 토큰갯수 나타내는 텍스트
        public Text healthText; // player채력 나타내는 텍스트

        [SerializeField]
        private GameObject timer;

        [SerializeField]
        private CameraShake cs;

        [SerializeField]
        private CanvasComponents canvas;

        [SerializeField]
        private SpriteRenderer backgroundSprite;

        [SerializeField]
        private GameObject optionBlackScreen;

        private Sprite[] backgroundSpriteArr;
        private bool gamePause = true; // false 게임 진행, true 게임 멈춤
        private readonly float[] gameSpeedMultiplier = new float[3] { 1f, 2f, 4f };
        private int gameSpeedMultiplierIndex;
        private int checkPause;
        private int NowPause;
        public RoundNum round; //{ get; private set; }
        public EnemySpawner Spawner => GetComponent<EnemySpawner>();
        public WaveController Wave => GetComponent<WaveController>();
        public Grid Grid => GetComponent<Grid>();
        public RelicManager RelicManager = new();
        public IReadOnlyList<Relic> Relics => RelicManager.OwnRelics;
        public float playSpeed => gameSpeedMultiplier[gameSpeedMultiplierIndex];
        public int tsumoToken { get; private set; }
        public int playerHealth { get; set; } = 25000;
        public int RagePoint { get; set; } = 0;
        public int[] CheongIlSaekCount { get; set; } = new int[3] { 0, 0, 0 };
        // public GlobalRelicStat GlobalRelicStat { get; set; } = new();

        [Header("DEBUG")]
        public bool DEBUG_MODE;
        public bool MONEY_CHEAT;
        public bool HAI_CHEAT;
        public HaiType HAI_CHEAT_SPEC_TYPE;
        public int HAI_CHEAT_SPEC_NUM;

        private void Start()
        {
            //canvas.Buttons[3].AddListenerOnly(() => canvas.YakuInst.ShowInstruction());
            backgroundSpriteArr = ResourceDictionary.GetAll<Sprite>("Background");
            if (!DEBUG_MODE)
                InitGame();
        }
        private void Update()
        {
            if (canvas.SpeedButtons[0].isDown)
                canvas.ChangeSpeedButtonImage(0, 2 * gameSpeedMultiplierIndex + 1);

            if(canvas.SpeedButtons[1].isDown)
                 canvas.ChangeSpeedButtonImage(1, 2 * checkPause + 7);
            
            if(canvas.SpeedButtons[2].isDown)
                 canvas.ChangeSpeedButtonImage(2, 13);

        }

        private void ResetSpeedButtons()
        {
            canvas.ChangeSpeedButtonImage(0, 0);
            canvas.ChangeSpeedButtonImage(1, 8);
            canvas.ChangeSpeedButtonImage(2, 12);

            canvas.SpeedButtons[0].AddListenerOnly(() =>
            {
                gameSpeedMultiplierIndex = (gameSpeedMultiplierIndex + 1) % 3;
                canvas.ChangeSpeedButtonImage(0, 2*gameSpeedMultiplierIndex);
            });
            canvas.SpeedButtons[1].AddListenerOnly(() =>
            {
                gamePause = !gamePause;
                if (gamePause)
                {
                    checkPause = 0;
                    canvas.ChangeSpeedButtonImage(1, 6);
                }
                else
                {
                    if (gameSpeedOnOff == 0)
                    {
                        checkPause = 0;
                        canvas.ChangeSpeedButtonImage(1, 6);
                        gamePause = true;
                        gameSpeedOnOff = 1;
                    }
                    else
                    {
                        checkPause = 2;
                        canvas.ChangeSpeedButtonImage(1, 10);
                    }
                }
            });
            canvas.SpeedButtons[2].AddListenerOnly(() =>
            {

                if (!optionBlackScreen.activeSelf)
                {
                    optionBlackScreen.SetActive(true);
                    canvas.YakuInst.RemoveInstruction();
                    NowPause = gameSpeedOnOff;
                    gameSpeedOnOff = 0;
                    optionOnOff = 0;
                }
                else
                {
                    optionBlackScreen.SetActive(false);
                    gameSpeedOnOff = NowPause;
                    optionOnOff = 1;
                }

                canvas.ChangeSpeedButtonImage(2, 12);
                
            });

            canvas.OptionButtons[0].AddListenerOnly(() =>
            {
                optionBlackScreen.SetActive(false);
                canvas.YakuInst.ShowInstruction();
            });
        }

        private void ResetGame()
        {
            Grid.ResetGame();
            ResetSpeedButtons();
            NextRound();
            tsumoToken = 6;
            //DEBUG
            if (MONEY_CHEAT) tsumoToken = 5000;
            playerHealth = 25000;
            tsumoTokenText.text = "" + tsumoToken;
            healthText.text = "" + playerHealth;
            RelicManager.Refresh(true);
        }

        private void InitGame()
        {
            Grid.InitGame();
            ResetGame();
        }

        public void PlusTsumoToken(int GetToken)
        {
            tsumoToken += GetToken;
            tsumoTokenText.text = "" + tsumoToken;
            //Grid.RefreshLockedCellsImage();
        }

        public bool MinusTsumoToken(int UseToken)
        {
            if (tsumoToken < UseToken)
            {
                return false;
            }

            tsumoToken -= UseToken;
            tsumoTokenText.text = "" + tsumoToken;
            //Grid.RefreshLockedCellsImage();
            return true;
        }

        public void OnEnemyCreate(EnemyController enemy)
        {
            Spawner.EnemyList.Add(enemy);
        }

        public void OnEnemyDestroy(EnemyController enemy)
        {
            if (enemy.bossType.HasFlag(EnemyController.BossType.Split))
            {
                Spawner.BossSplit(5000, 0.2f, enemy.gameObject.transform);
            }
            for (int i = Spawner.EnemyList.Count - 1; i >= 0; i--)
            {
                if (Spawner.EnemyList[i] != enemy) continue;
                Spawner.EnemyList.RemoveAt(i);
                RagePoint -= (int)enemy.MaxHealth;
                RagePoint = RagePoint < 0 ? 0 : RagePoint;
                Destroy(enemy.gameObject);
                return;
            }
        }

        public void PlayerDamage(int damage)
        {
            playerHealth -= damage;
            RagePoint += damage;
            RagePoint = RagePoint > 10000 ? 10000 : RagePoint;
            healthText.text = "" + playerHealth;
            canvas.DamageOverlay.SetDamageOverlay(damage / 1500f);
            if (playerHealth <= 0) SceneManager.LoadScene("StartScene");

            StartCoroutine(cs.Shake(0.3f, 0.005f));
        }

        public void PlayerHeal(int healAmount)
        {
            playerHealth += healAmount;
            healthText.text = "" + playerHealth;
        }

        public void NextRound()
        {
            if (gamePause)
            {
                checkPause = 1;
                gameSpeedOnOff = 0;
                canvas.ChangeSpeedButtonImage(1, 8);
            }

            if (!round.NextRound()) Wave.WaveStart(round.season * 16 + round.wind * 4 + round.number);
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

            roundText.text = seasonText + "/" + windText + (round.number + 1) + "국";
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

            
            if (season > 3) return true;
            var round = RoundManager.Inst;
            round.RagePoint += round.RelicManager[typeof(RageRelic)] * 2000;
            return false;
        }

        public void NumberPlus()
        {
            number++;
        }
    }
}
