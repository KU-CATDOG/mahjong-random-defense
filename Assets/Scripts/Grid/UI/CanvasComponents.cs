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
        public GameObject TrashCan;
        private Sprite[] buttonSpriteArr;
        private Sprite[] speedButtonSpriteArr;

        private void Start()
        {
            screenOnButton.AddListener(() =>
            {
                SetBlackScreen(!BlackScreen.activeSelf);
                RoundManager.Inst.Grid.ResetScreenButton();
                });
            TrashCan.SetActive(false);
        }

        public void SetBlackScreen(bool isOn)
        {
            BlackScreen.SetActive(isOn);
        }

        public void ChangeButtonImage(int btnNum, int imgNum, string path)
        {
            buttonSpriteArr = ResourceDictionary.GetAll<Sprite>(path);
            Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[imgNum];
        }

        public void ChangeSpeedButtonImage(int btnNum, int imgNum)
        {
            speedButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/square_icon");
            SpeedButtons[btnNum].GetComponent<Image>().sprite = speedButtonSpriteArr[imgNum];
        }
    }
}
