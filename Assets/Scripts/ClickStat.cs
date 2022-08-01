using UnityEngine;
using UnityEngine.EventSystems;

namespace MRD
{
    public class ClickStat : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TowerStatImageController towerStatImageController;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (towerStatImageController.isYakuTextEnabled)
                towerStatImageController.RemoveYakuText();
            else
                towerStatImageController.SetYakuText();
        }
    }
}
