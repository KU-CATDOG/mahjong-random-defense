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

            switch (imgNum)
            {
                case 0:
                    Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[0];
                    break;
                case 1:
                    Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[1];
                    break;
                case 2:
                    Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[2];
                    break;
                case 3:
                    Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[3];
                    break;
                case 4:
                    Buttons[btnNum].GetComponent<Image>().sprite = buttonSpriteArr[4];
                    break;
            }
        }
    }
}
