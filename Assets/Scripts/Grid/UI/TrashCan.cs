using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    public class TrashCan : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Sprite[] trashCanSpriteArr;
        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponent<Image>().sprite = trashCanSpriteArr[1];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetComponent<Image>().sprite = trashCanSpriteArr[0];
        }
        public void OnDrop(PointerEventData eventData)
        {
            RoundManager.Inst.Grid.DeleteTower(UICell.tempGrid);
            GetComponent<Image>().sprite = trashCanSpriteArr[0];
        }
    }
}
