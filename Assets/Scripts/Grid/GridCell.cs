using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class GridCell : MonoBehaviour
    {
        public AttackCell pair { get; private set; }
        public XY coordinate { get; private set; }

        public void Init(AttackCell attackCellInstance, XY coord)
        {
            pair = attackCellInstance;
            coordinate = coord;
        }
    }
}