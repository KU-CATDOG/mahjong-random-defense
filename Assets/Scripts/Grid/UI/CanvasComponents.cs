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

        private void Start()
        {
            screenOnButton.AddListener(() => SetBlackScreen(true));
        }

        public void SetBlackScreen(bool isOn)
        {
            BlackScreen.SetActive(isOn);
            screenOnButton.gameObject.SetActive(!isOn);
        }
    }
}
