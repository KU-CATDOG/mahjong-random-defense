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
        private GameObject towerImage;

        private GameObject set;

        public Text YakuName;
        public Text YakuCondition;

        private bool check = false;

        private void Start()
        {
            instruction.SetActive(false);
            Debug.Log("Start!");
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
                Instantiate(set, transform.position, Quaternion.identity);

                YakuName.text = "Name";
                YakuCondition.text = "Condition";
                //towerImage = ResourceDictionary.Get<GameObject>();
            }
        }

    }
}
   
