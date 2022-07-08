using UnityEngine;

namespace MRD
{
    public class GridCell : MonoBehaviour
    {
        public AttackCell Pair { get; private set; }

        public XY Coordinate { get; private set; }

        public void Init(AttackCell attackCellInstance, XY coord)
        {
            Pair = attackCellInstance;
            Coordinate = coord;
        }
    }
}