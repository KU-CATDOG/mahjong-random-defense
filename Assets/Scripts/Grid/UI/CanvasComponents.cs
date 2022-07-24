using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class CanvasComponents : MonoBehaviour
    {
        public RectTransform GridParent;
        public GameObject BlackScreen;
        public ClickUI[] Buttons;
        [SerializeField]
        private ClickUI screenOnButton;
        [SerializeField]
        public ClickUI[] SpeedButtons;
        private Sprite[] buttonSpriteArr;
        private Sprite[] speedButtonSpriteArr;
        public ClickUI UpgradeButton;
        public Text UpgradeText;
        public DamageOverlayController DamageOverlay;

        private void Start()
        {
            screenOnButton.AddListener(() => SetBlackScreen(!BlackScreen.activeSelf));
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
            speedButtonSpriteArr = ResourceDictionary.GetAll<Sprite>("ButtonSprite/SmallButton");
            SpeedButtons[btnNum].GetComponent<Image>().sprite = speedButtonSpriteArr[imgNum];
        }
    }
}
