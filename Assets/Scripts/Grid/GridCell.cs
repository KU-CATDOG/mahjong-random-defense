using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    public class GridCell : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform Rect => GetComponent<RectTransform>();
        public Tower Pair { get; private set; }

        public XY Coordinate { get; private set; }

        private Sprite[] gridSprites;

        private GridCellState _state;
        public GridCellState State
        {
            get => _state;
            set => ChangeState(value);
        }

        public void Init(Tower tower, XY coord)
        {
            Pair = tower;
            Coordinate = coord;
            gridSprites = ResourceDictionary.GetAll<Sprite>("TowerSprite/grid_border");
        }

        public void ChangeState(GridCellState nextState)
        {
            _state = nextState;
            GetComponent<Image>().sprite = gridSprites[State switch
            {
                GridCellState.NotChoosable => 3,
                GridCellState.Idle => Pair.TowerStat.TowerInfo == null ? 3 : 0,
                GridCellState.Choosable => 1,
                GridCellState.Choosed => 2,
                _ => 0
            }];
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (State)
            {

                case GridCellState.Choosable:
                    State = GridCellState.Choosed;
                    RoundManager.Inst.Grid.SelectCell(this);                    
                    break;
                case GridCellState.Choosed:
                    State = GridCellState.Choosable;
                    RoundManager.Inst.Grid.DeselectCell(this);
                    break;
                default:
                    break;
            }
        }
    }

    public enum GridCellState
    {
        Idle, Choosable, NotChoosable, Choosed
    }
}