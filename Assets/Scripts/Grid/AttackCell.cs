using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class AttackCell : MonoBehaviour
    {
        public TowerInfo tower { get; private set; }
        public GridCell pair { get; private set; }
        public XY coordinate { get; private set; }

        public void Init(GridCell gridCellInstance, XY coord)
        {
            pair = gridCellInstance;
            coordinate = coord;
            pair.Init(this, coord);
        }
    }
}