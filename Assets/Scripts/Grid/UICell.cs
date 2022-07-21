using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    public abstract class UICell : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform Rect => GetComponent<RectTransform>();

        private static Sprite[] gridSprites;

        private GridCellState _state;
        public GridCellState State
        {
            get => _state;
            set => ChangeState(value);
        }

        public virtual TowerInfo TowerInfo => null;

        public virtual void Init()
        {
            
        }
        public static void LoadSprites()
        {
            gridSprites = ResourceDictionary.GetAll<Sprite>("TowerSprite/grid_border");
        }
        public void ChangeState(GridCellState nextState)
        {
            _state = nextState;
            GetComponent<Image>().sprite = gridSprites[State switch
            {
                GridCellState.NotChoosable => 3,
                GridCellState.Idle => TowerInfo == null ? 3 : 0,
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
        private Image[] SetGridLayers(int n)
        {
            Image[] images = new Image[n];

            int childNum = transform.childCount;

            Transform backGround = transform.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(backGround, transform);

            int newChildNum = transform.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = transform.GetChild(i);
                tmp.gameObject.SetActive(true);
                images[i] = tmp.GetComponent<Image>();
                images[i].rectTransform.anchoredPosition = Vector2.zero;
            }

            for (int i = n; i < newChildNum; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            return images;
        }
        public void ApplyTowerImage()
        {
            if (TowerInfo == null)
            {
                SetGridLayers(0);
                return;
            }
            int count = TowerInfo.Hais.Count;

            if (TowerInfo is SingleHaiInfo or MentsuInfo)
            {
                HaiType type = TowerInfo.Hais[0].Spec.HaiType;

                int number = type is HaiType.Kaze or HaiType.Sangen ?
                    TowerInfo.Hais[0].Spec.Number + 1 :
                    TowerInfo.Hais[0].Spec.Number;
                // grid tower
                Image[] images = count switch
                {
                    1 => SetGridLayers(2),
                    _ => SetGridLayers(3)        //2, 3, 4
                };
                images[0].sprite = Tower.SingleMentsuSpriteDict[$"BackgroundHai{count}"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type.ToString() + number.ToString()];
                images[1].rectTransform.anchoredPosition = new Vector2(-0.0315f * (count - 1), 0);

                if (count > 1)
                {
                    images[2].sprite = TowerInfo switch
                    {
                        KoutsuInfo koutsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(koutsu.IsMenzen ? 7 : 6)}"],
                        ShuntsuInfo shuntsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(shuntsu.IsMenzen ? 5 : 4)}"],
                        KantsuInfo kantsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(kantsu.IsMenzen ? 3 : 2)}"],
                        _ => Tower.SingleMentsuSpriteDict["Mentsu1"],
                    };
                }
            }
            else if (TowerInfo is TripleTowerInfo && this is GridCell cell)
            {
                //TowerOption 중에서 TowerImageOption만 받아오기
                var towerOptions = cell.Pair.TowerStat.Options;

                List<TowerImageOption> towerImageOptions = new();

                foreach (var option in towerOptions.Values)
                {
                    if (option.GetType().IsSubclassOf(typeof(TowerImageOption)))
                    {
                        towerImageOptions.Add((TowerImageOption)option);
                    }
                }

                //받아온 TowerImageOption에서 Images 받아와서 imageList에 저장
                List<(int index, int order)> imagesList = new();

                foreach (var towerImageOption in towerImageOptions)
                {
                    var images = towerImageOption.Images;

                    foreach ((var i, var o) in images)
                    {
                        imagesList.Add((i, o));
                    }
                }

                var gridImages = SetGridLayers(imagesList.Count + 1);
                gridImages[0].sprite = Tower.TripleSpriteList[0];
                int layerCount = 1;
                foreach (var (index, _) in imagesList.OrderBy(x => x.order))
                {
                    gridImages[layerCount++].sprite = Tower.TripleSpriteList[index];
                }
            }
        }
    }

    public enum GridCellState
    {
        Idle, Choosable, NotChoosable, Choosed
    }
}