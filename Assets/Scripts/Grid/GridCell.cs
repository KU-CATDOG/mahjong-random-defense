namespace MRD
{
    public class GridCell : UICell
    {
        public Tower Pair { get; private set; }

        public XY Coordinate { get; private set; }

        public override TowerInfo TowerInfo => Pair.TowerStat.TowerInfo;

        public void Init(Tower tower, XY coord)
        {
            Pair = tower;
            Coordinate = coord;
            base.Init();
        }
    }
}
