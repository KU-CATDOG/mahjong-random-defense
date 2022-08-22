using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace MRD
{
    public class Tutorial : MonoBehaviour
    {
        private Sprite[] tutorialBackgroundArr;
        private int num = 0;

        public ClickUI[] ScreenTouch;
        public GameObject TutorialScreen;


        void Start()
        {
            tutorialBackgroundArr = ResourceDictionary.GetAll<Sprite>("Background/tutorial");
            ShowTutorial();
        }

        private void ShowTutorial()
        {
            ScreenTouch[0].AddListenerOnly(() =>
            {
                if (num > 0) num -= 1;
                ShowNextBackground(num);
            });
            ScreenTouch[1].AddListenerOnly(() =>
            {
                if (num == tutorialBackgroundArr.Length - 1) SceneManager.LoadScene("StartScene");
                if (num < tutorialBackgroundArr.Length - 1) num += 1;
                ShowNextBackground(num);
            });
        }

        private void ShowNextBackground(int n)
        {
            TutorialScreen.GetComponent<Image>().sprite = tutorialBackgroundArr[n];
            ShowTutorial();
        }
    }
}
