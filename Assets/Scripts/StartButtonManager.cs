using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MRD
{
    public class StartButtonManager : MonoBehaviour
    {
        public ClickUI[] Buttons;
        public YakuInstruction YakuInst;
        public RelicInstruction RelicInst;
        public BasicInstruction BasicInst;
        [SerializeField]
        private GameObject instructions;

        private void Start()
        {
            Buttons[0].AddListenerOnly(() =>
            {
                SoundManager.Inst.PlaySFX("ClickUIButton");
                SceneManager.LoadScene("GameSceneRenewal");
            });

            Buttons[1].AddListenerOnly(() =>
            {
                SoundManager.Inst.PlaySFX("ClickUIButton");
                SceneManager.LoadScene("TutorialScene");
            });

            Buttons[2].AddListenerOnly(() =>
            {
                instructions.SetActive(true);
                BasicInst.ShowInstruction();
                SoundManager.Inst.PlaySFX("ClickUIButton");
            });

            Buttons[3].AddListenerOnly(() =>
            {
                RelicInst.RemoveInstruction();
                YakuInst.RemoveInstruction();
                BasicInst.ShowInstruction();
                SoundManager.Inst.PlaySFX("ClickInstButton");
            });

            Buttons[4].AddListenerOnly(() =>
            {
                BasicInst.RemoveInstruction();
                RelicInst.RemoveInstruction();
                YakuInst.ShowInstruction();
                SoundManager.Inst.PlaySFX("ClickInstButton");
            });

            Buttons[5].AddListenerOnly(() =>
            {
                BasicInst.RemoveInstruction();
                YakuInst.RemoveInstruction();
                RelicInst.ShowInstruction();
                SoundManager.Inst.PlaySFX("ClickInstButton");
            });

            Buttons[6].AddListenerOnly(() =>
            {
                BasicInst.RemoveInstruction();
                YakuInst.RemoveInstruction();
                RelicInst.RemoveInstruction();
                instructions.SetActive(false);
                SoundManager.Inst.PlaySFX("ClickInstButton");
            });
        }
       
    }
}
