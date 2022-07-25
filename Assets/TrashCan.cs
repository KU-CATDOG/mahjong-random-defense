using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
