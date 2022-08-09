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
        private GameObject set;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private List<InstructionScriptable> Insts;

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
                //set = ResourceDictionary.Get<GameObject>("Prefabs/InstuctionSet");
                Instantiate(set, content);

                //Name.text = Insts[i].Name;
                //Condition.text = Insts[i].Condition;
                //Image?
   
            }
        }
    }
}
   
