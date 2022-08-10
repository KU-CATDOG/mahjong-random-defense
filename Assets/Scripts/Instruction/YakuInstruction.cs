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
        private bool created = false;

        private void Start()
        {
            instruction.SetActive(false);
        }

        public void ShowInstruction()
        {
            instruction.SetActive(true);
            if(!created) MakeInstruction();
            created = true;
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
                var c = getset.GetComponent<SetComponents>();
                c.Name.text = Insts[i].Name;
                c.Condition.text = Insts[i].Condition;
                
                for(int j = 0; j < Insts[i].Image.Length; j++)
                {
                    var TI = Instantiate(c.TimageHolder, c.TimageParent);
                    TI.GetComponent<RectTransform>().anchoredPosition3D += new Vector3 (j*1.2f, 0, 0);
                    //TI.transform.parent = c.TimageParent;
                    //Instantiate(getset.GetComponent<SetComponents>().Timage, getset.GetComponent<SetComponents>().TimageParent);

                    for (int k = 0; k < Insts[i].Image[j].sprite.Length; k++)
                    {
                        var Im = Instantiate(c.Timage, TI.transform);
                        Im.GetComponent<Image>().sprite = Insts[i].Image[j].sprite[k];
                    }
                }
            }
        }
    }
}
   
