using UnityEngine;
using UnityEngine.EventSystems;

namespace MRD
{
    public class TrashCan : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            UICell.tempGrid.Pair.SetTower(null);
            UICell.tempGrid.ApplyTowerImage();
            UICell.tempGrid.Pair.ApplyTowerImage();
            RoundManager.Inst.Grid.filledCellCount--;
        }
    }
}
