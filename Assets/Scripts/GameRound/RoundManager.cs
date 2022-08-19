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
        public Text refreshCostText; // 리롤 코스트 나타내는 텍스트

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

        [SerializeField]
        private GameObject instructions;

        private Sprite[] backgroundSpriteArr;
        private bool gamePause = true; // false 게임 진행, true 게임 멈춤
        private readonly float[] gameSpeedMultiplier = new float[3] { 1f, 2f, 4f };
        private int gameSpeedMultiplierIndex;
        private int checkPause;
        private int NowPause;
        
        public RoundNum round; //{ get; private set; }
        public EnemySpawner Spawner => GetComponent<EnemySpawner>();
        public ShopManager Shop => GetComponent<ShopManager>();
        public WaveController Wave => GetComponent<WaveController>();
        public Grid Grid => GetComponent<Grid>();
        public RelicManager RelicManager = new();
        public OwnRelicImageController ownRelicImageController;
        public IReadOnlyList<Relic> Relics => RelicManager.OwnRelics;
        public float playSpeed => gameSpeedMultiplier[gameSpeedMultiplierIndex];
        public int tsumoToken { get; private set; }
        public int playerHealth { get; set; } = 25000;
        public int RagePoint { get; set; } = 0;
        public int[] CheongIlSaekCount { get; set; } = new int[3] { 0, 0, 0 };

        [SerializeField, Header("Balance")]
        private int initToken = 6;
        [SerializeField]
        private int roundToken = 6;
        [SerializeField]
        private float enemyHealthAdder = 0.05f;
        [SerializeField]
        private float enemyHealthPower = 1.5f;
        public float EnemyHealthAdder => enemyHealthAdder;
        public float EnemyHealthPower => enemyHealthPower;

        [Header("DEBUG")]
        public bool DEBUG_MODE;
        public bool MONEY_CHEAT;
        public bool HAI_CHEAT;
        public HaiType HAI_CHEAT_SPEC_TYPE;
        public int HAI_CHEAT_SPEC_NUM;
        private bool isShopOn = false;

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
                 canvas.ChangeSpeedButtonImage(2, 1);

        }

        private void ResetSpeedButtons()
        {
            canvas.ChangeSpeedButtonImage(0, 0);
            canvas.ChangeSpeedButtonImage(1, 6);
            canvas.ChangeSpeedButtonImage(2, 0);

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
                    checkPause = 1;
                    canvas.ChangeSpeedButtonImage(1, 8);
                }
                else
                {
                    if (gameSpeedOnOff == 0)
                    {
                        checkPause = 1;
                        canvas.ChangeSpeedButtonImage(1, 8);
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
                    canvas.RelicInst.RemoveInstruction();
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

                canvas.ChangeSpeedButtonImage(2, 0);
                
            });
        }
        private void ResetButton()
        {
            canvas.ShopButton.AddListenerOnly(() =>
            {
                if(!isShopOn)
                {
                    canvas.shopBlackScreen.SetActive(true);
                    isShopOn = true;
                    ownRelicImageController.ShowOwnRelics();
                }
                else
                {
                    RoundManager.Inst.Grid.State = EditState.Idle;
                    RoundManager.Inst.Grid.ResetGrid();
                    if (RoundManager.Inst.Grid.doraList.isShowingDora)
                    {
                        RoundManager.Inst.Grid.doraList.ResetDoraImage();
                    }
                    canvas.shopBlackScreen.SetActive(false);
                    isShopOn = false;
                    ownRelicImageController.RemoveOwnReclics();

                }
                // canvas.BlackScreen.SetActive(false);
            });

            //canvas.OptionButtons[0].AddListenerOnly(() =>
            //{
            //    optionBlackScreen.SetActive(false);
            //    canvas.YakuInst.ShowInstruction();
            //});

            canvas.OptionButtons[1].AddListenerOnly(() =>
            {
                optionBlackScreen.SetActive(false);
                instructions.SetActive(true);
                canvas.BasicInst.ShowInstruction();
            });

            canvas.InstButtons[0].AddListenerOnly(() =>
            {
                canvas.RelicInst.RemoveInstruction();
                canvas.YakuInst.RemoveInstruction();
                canvas.BasicInst.ShowInstruction();
            });

            canvas.InstButtons[1].AddListenerOnly(() =>
            {
                canvas.BasicInst.RemoveInstruction();
                canvas.RelicInst.RemoveInstruction();
                canvas.YakuInst.ShowInstruction();
            });

            canvas.InstButtons[2].AddListenerOnly(() =>
            {
                canvas.BasicInst.RemoveInstruction();
                canvas.YakuInst.RemoveInstruction();
                canvas.RelicInst.ShowInstruction();
            });

            canvas.InstButtons[3].AddListenerOnly(() =>
            {
                canvas.BasicInst.RemoveInstruction();
                canvas.YakuInst.RemoveInstruction();
                canvas.RelicInst.RemoveInstruction();
                instructions.SetActive(false);
            });
        }

        private void ResetGame()
        {
            Grid.ResetGame();
            ResetSpeedButtons();
            ResetButton();
            NextRound();
            tsumoToken = initToken;
            //DEBUG
            if (MONEY_CHEAT) tsumoToken = 5000;
            playerHealth = 25000;
            tsumoTokenText.text = "" + tsumoToken;
            healthText.text = "" + playerHealth;
            Shop.ResetShop();
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
                checkPause = 0;
                gameSpeedOnOff = 0;
                canvas.ChangeSpeedButtonImage(1, 6);
            }

            int prevSeason = round.season;
            if (!round.NextRound()) Wave.WaveStart(round.season * 16 + round.wind * 4 + round.number);

            if (round.season != prevSeason) RelicManager.ResetRefreshCost();

            PlusTsumoToken(roundToken + RelicManager[typeof(PensionRelic)]);

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
            Grid.UpdateRichiOnRoundTick();
        }

        public void AttachTimer(float targetTime, int targetCount, Tower coroutineOwner, Timer.OnTick onTick)
        {
            var newTimer = Instantiate(timer);
            newTimer.transform.SetParent(transform);
            newTimer.GetComponent<Timer>().Init(targetTime, targetCount, coroutineOwner, onTick);
        }

        public void OnDoraAnimation(HaiType type, int num)
        {
            Instantiate(ResourceDictionary.Get<GameObject>("Prefabs/DoraAnimation"), canvas.BlackScreen.transform).GetComponent<DoraAnimator>().Init(type,num,((RectTransform)canvas.DoraButton.transform).anchoredPosition);
        }
    }

    public struct RoundNum
    {
        public int season { get; private set; }
        public int wind { get; private set; }
        public int number { get; private set; }


        public bool NextRound()
        {
            var round = RoundManager.Inst;
            bool isWindChange = false;
            if (number > 3)
            {
                number = 0;
                wind++;
                isWindChange = true;
            }
            if (wind > 3)
            {
                wind = 0;
                season++;
                isWindChange = true;
            }
            if (season > 3) return true;
            if(isWindChange)
                round.Grid.UpdateAllTower(false);

            round.RagePoint += round.RelicManager[typeof(RageRelic)] * 2000;
            return false;
        }

        public void NumberPlus()
        {
            number++;
        }
    }
}
