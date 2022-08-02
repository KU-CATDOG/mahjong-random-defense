using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class YakuInstruction : MonoBehaviour
    {
        [SerializeField]
        private GameObject backGround;
        private GameObject instruction;
        private GameObject towerImage;

        private GameObject set;

        public Text YakuName;
        public Text YakuCondition;

        public void ShowInstruction()
        {
            backGround.SetActive(true);
            instruction.SetActive(true);
        }

        public void RemoveInstruction()
        {
            backGround.SetActive(false);
            instruction.SetActive(false);
            MakeInstruction();
        }

        private void MakeInstruction()
        {
            //set 복제
            //각각의 set의 text, image에 역 이름, 역 설명, 타워 이미지 넣기

            //for, 전체 역 개수만큼
            Instantiate(set, transform.position, Quaternion.identity);

            YakuName.text = "Name";
            YakuCondition.text = "Condition";
            //towerImage = ResourceDictionary.Get<GameObject>();
        }
    }
}
   
