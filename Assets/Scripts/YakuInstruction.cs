using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class YakuInstruction : MonoBehaviour
    {
        [SerializeField]
        private GameObject instruction;
        [SerializeField]
        private GameObject set;
        [SerializeField]
        private Transform content;

        private GameObject towerImage;

        public Text YakuName;
        public Text YakuCondition;

        private void Start()
        {
            instruction.SetActive(false);
            Debug.Log("Start!");

            ShowInstruction();
        }

        private void ShowInstruction()
        {
            instruction.SetActive(true);
            MakeInstruction();
        }

        private void RemoveInstruction()
        {
            instruction.SetActive(false);
        }

        private void MakeInstruction()
        {
            //set 복제
            //각각의 set의 text, image에 역 이름, 역 설명, 타워 이미지 넣기

            for(int i = 0; i < 4; i++)
            {
                Instantiate(set, content);
                Debug.Log(i);

                //YakuName.text = "Name";
                //YakuCondition.text = "Condition";
                //towerImage = ResourceDictionary.Get<GameObject>();
            }
        }

    }
}
   
