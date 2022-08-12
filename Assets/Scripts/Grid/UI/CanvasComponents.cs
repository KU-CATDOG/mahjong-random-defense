using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class CanvasComponents : MonoBehaviour
    {
        public RectTransform GridParent;
        public RectTransform FuroParent;
        public GameObject BlackScreen;
        public ClickUI[] Buttons;
        public YakuInstruction YakuInst;

        [SerializeField]
        public ClickUI screenOnButton;

        [SerializeField]
        public ClickUI[] SpeedButtons;
        public RectTransform JoinAnimator;

        public ClickUI UpgradeButton;
        public Text UpgradeText;
        public DamageOverlayController DamageOverlay;
        public ClickUI ResetButton;
        public ClickUI ShopButton;
        public GameObject TrashCan;
        public ClickUI[] OptionButtons;
        public ClickUI DoraButton;
        private Sprite[] buttonSpriteArr;
        private Sprite[] speedButtonSpriteArr;
        private Sprite[] middleButton;
        private int middleButtonNum;

        private void Start()
        {
            screenOnButton.AddListener(() =>
            {
                if (!RoundManager.Inst.shopBlackScreen.activeSelf)
                {
                    SetBlackScreen(!BlackScreen.activeSelf);
                    RoundManager.Inst.Grid.ResetScreenButton();
                }
            });
            TrashCan.SetActive(false);
            Buttons[4].gameObject.SetActive(false);
            middleButton = ResourceDictionary.GetAll<Sprite>("UiSprite/middle_button");
        }

        private void Update()
        {
            if (screenOnButton.isDown == true)
                screenOnButton.GetComponent<Image>().sprite = middleButton[middleButtonNum + 1];
            else
                screenOnButton.GetComponent<Image>().sprite = middleButton[middleButtonNum];
        }

        public void SetBlackScreen(bool isOn)
        {
            BlackScreen.SetActive(isOn);
            middleButtonNum = isOn ? 2 : 0;
        }

        public void ChangeButtonImage(int btnNum, int imgNum, string path)
        {
            buttonSpriteArr = ResourceDictionary.GetAll<Sprite>(path);
            Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[imgNum];
        }

        public void ChangeSpeedButtonImage(int btnNum, int imgNum)
        {
            speedButtonSpriteArr = btnNum == 2 ? ResourceDictionary.GetAll<Sprite>("UISprite/square_icon") : ResourceDictionary.GetAll<Sprite>("UISprite/playspeed_button");
            SpeedButtons[btnNum].GetComponent<Image>().sprite = speedButtonSpriteArr[imgNum];
        }
    }
}
