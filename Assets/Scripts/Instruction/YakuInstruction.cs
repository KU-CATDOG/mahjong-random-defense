using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MRD
{
    public class YakuInstruction : MonoBehaviour
    {
        [SerializeField]
        private GameObject instruction;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private List<InstructionScriptable> Insts;

        private GameObject set;
        private Image TowerImage;

        private void Start()
        {
            instruction.SetActive(false);
        }

        public void ShowInstruction()
        {
            instruction.SetActive(true);
            MakeInstruction();
        }

        public void RemoveInstruction()
        {
            instruction.SetActive(false);
        }

        private void MakeInstruction()
        {
            for(int i = 0; i < Insts.Count; i++)
            {
                set = ResourceDictionary.Get<GameObject>("Prefabs/InstSet");
                var getset = Instantiate(set, content);
                getset.GetComponent<SetComponents>().Name.text = Insts[i].Name;
                getset.GetComponent<SetComponents>().Condition.text = Insts[i].Condition;
                //Image?
            }
        }
    }
}
   
