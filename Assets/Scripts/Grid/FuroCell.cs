namespace MRD
{
    public class FuroCell : UICell
    {
        private SingleHaiInfo haiInfo;

        public override TowerInfo TowerInfo => haiInfo;

        public void SetTowerInfo(SingleHaiInfo info) => haiInfo = info;
    }
}
