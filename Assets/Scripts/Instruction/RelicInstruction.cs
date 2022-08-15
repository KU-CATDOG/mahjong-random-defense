using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MRD
{
    public class RelicInstruction : MonoBehaviour
    {
        [SerializeField]
        private GameObject instruction;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private List<RelicInstructionScriptable> Insts;

        private GameObject set;
        private bool created = false;
        private Sprite[] rankSpriteArr;

        private void Start()
        {
            instruction.SetActive(false);
            rankSpriteArr = ResourceDictionary.GetAll<Sprite>("UISprite/relic_border");
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
                set = ResourceDictionary.Get<GameObject>("Prefabs/InstSetRelic");
                var getset = Instantiate(set, content);
                var c = getset.GetComponent<SetRelicComponents>();

                switch (Insts[i].Rank)
                {
                    case "S":
                        getset.GetComponent<Image>().sprite = rankSpriteArr[3];
                        break;
                    case "A":
                        getset.GetComponent<Image>().sprite = rankSpriteArr[2];
                        break;
                    case "B":
                        getset.GetComponent<Image>().sprite = rankSpriteArr[1];
                        break;
                    case "C":
                        getset.GetComponent<Image>().sprite = rankSpriteArr[0];
                        break;
                }
                c.Name.text = Insts[i].Name;
                c.Info.text = Insts[i].Info;
                c.RelicImage.GetComponent<Image>().sprite = Insts[i].Image;
            }
        }
    }
}
   
