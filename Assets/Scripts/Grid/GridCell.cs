using UnityEngine;
namespace MRD
{
    public class GridCell : UICell
    {
        public Tower Pair { get; private set; }

        public XY Coordinate { get; private set; }

        public override TowerInfo TowerInfo => Pair.TowerStat.TowerInfo;

        [SerializeField]
        private RectTransform RichiParent;

        [SerializeField]
        private UnityEngine.UI.Image RichiNumber;
        public static Sprite[] RichiSpriteList;

        public void Init(Tower tower, XY coord)
        {
            Pair = tower;
            Coordinate = coord;
            base.Init();
        }
        public void UpdateRichiState(bool value)
            => RichiParent.gameObject.SetActive(value);
        public void UpdateRichiImage(int number)
        {
            if(RichiSpriteList == null)
                RichiSpriteList = ResourceDictionary.GetAll<UnityEngine.Sprite>("TowerSprite/richi_numbers");
            RichiNumber.sprite = RichiSpriteList[number];
        }
    }
}
