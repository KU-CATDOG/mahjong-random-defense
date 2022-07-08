using UnityEngine;

namespace MRD
{
    public class Tower : MonoBehaviour
    {
        public TowerInfo TowerInfo { get; private set; }
        public GridCell Pair { get; private set; }

        public XY Coordinate => Pair.Coordinate;

        public void Init(GridCell gridCellInstance, XY coord)
        {
            Pair = gridCellInstance;
            Pair.Init(this, coord);
        }
    }
}