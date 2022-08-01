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

        [SerializeField]
        private ClickUI screenOnButton;

        [SerializeField]
        public ClickUI[] SpeedButtons;

        public ClickUI UpgradeButton;
        public Text UpgradeText;
        public DamageOverlayController DamageOverlay;
        public ClickUI ResetButton;
        public GameObject TrashCan;
        private Sprite[] buttonSpriteArr;
        private Sprite[] speedButtonSpriteArr;

        private void Start()
        {
            screenOnButton.AddListener(() => SetBlackScreen(!BlackScreen.activeSelf));
            TrashCan.SetActive(false);
        }

        public void SetBlackScreen(bool isOn)
        {
            BlackScreen.SetActive(isOn);
        }

        public void ChangeButtonImage(int btnNum, int imgNum)
        {
            buttonSpriteArr = ResourceDictionary.GetAll<Sprite>("ButtonSprite/BigButton");
            Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[imgNum];
        }

        public void ChangeSpeedButtonImage(int btnNum, int imgNum)
        {
            speedButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("ButtonSprite/NewSmallButton");
            SpeedButtons[btnNum].GetComponent<Image>().sprite = speedButtonSpriteArr[imgNum];
        }
    }
}
