using UnityEngine;

namespace MRD
{
    public class GridCell : MonoBehaviour
    {
        public Tower Pair { get; private set; }

        public XY Coordinate { get; private set; }

        public void Init(Tower tower, XY coord)
        {
            Pair = tower;
            Coordinate = coord;
        }
    }
}