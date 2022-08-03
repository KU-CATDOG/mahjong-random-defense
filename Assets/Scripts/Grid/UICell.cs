using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    public abstract class UICell : MonoBehaviour, IPointerClickHandler, IDragHandler, IDropHandler, IBeginDragHandler,
        IEndDragHandler
    {
        private static Sprite[] gridSprites;

        public static Vector2 defaultPos;
        public static GridCell tempGrid;
        public int checker;

        private GridCellState _state;

        public RectTransform Rect => GetComponent<RectTransform>();

        public GridCellState State
        {
            get => _state;
            set => ChangeState(value);
        }

        public virtual TowerInfo TowerInfo => null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            defaultPos = transform.position;
            if(this is not FuroCell)
                tempGrid = (GridCell)this;
            if (this is GridCell cell && cell.TowerInfo != null && RoundManager.Inst.Grid.State is EditState.Idle/*&& State == GridCellState.Choosed*/)
            {
                transform.SetAsLastSibling();
                RoundManager.Inst.Grid.SetTrashCan(true);
                RoundManager.Inst.Grid.RemoveTowerStatImage();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (this is GridCell &&/* State == GridCellState.Choosed &&*/ RoundManager.Inst.Grid.State is EditState.Idle &&
                TowerInfo != null)
            {
                transform.position = eventData.position;
                GetComponent<Image>().raycastTarget = false;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (this is GridCell cell && tempGrid.TowerInfo != null && RoundManager.Inst.Grid.State is EditState.Idle/*tempGrid.State == GridCellState.Choosed*/)
            {
                var temp = TowerInfo;
                cell.Pair.SetTower(tempGrid.TowerInfo);
                tempGrid.Pair.SetTower(temp);

                cell.Pair.ApplyTowerImage();
                cell.ApplyTowerImage();

                tempGrid.Pair.ApplyTowerImage();
                tempGrid.ApplyTowerImage();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = defaultPos;
            GetComponent<Image>().raycastTarget = true;
            if (RoundManager.Inst.Grid.State is not EditState.Join)
            {
                RoundManager.Inst.Grid.DeselectCell(this);
                State = GridCellState.Idle;
            }
            //tempGrid.State = GridCellState.Idle;
            RoundManager.Inst.Grid.SetTrashCan(false);
            RoundManager.Inst.Grid.ResetSiblingIndex();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (State)
            {
                case GridCellState.Idle:
                    if (this is GridCell cell && cell.Pair.TowerStat.TowerInfo is not null)
                    {
                        State = GridCellState.Choosed;
                        RoundManager.Inst.Grid.SelectCell(this);
                        checker = 1;

                        RoundManager.Inst.Grid.SetTowerStatImage(cell);
                    }

                    break;

                case GridCellState.Choosable:
                    State = GridCellState.Choosed;
                    RoundManager.Inst.Grid.SelectCell(this);
                    break;
                case GridCellState.Choosed:
                    State = checker == 1 ? GridCellState.Idle : GridCellState.Choosable;
                    if (checker == 1)
                        RoundManager.Inst.Grid.RemoveTowerStatImage();
                    RoundManager.Inst.Grid.DeselectCell(this);
                    checker = 0;
                    break;
            }
            if (TowerInfo is null || State == GridCellState.NotChoosable)
                RoundManager.Inst.Grid.ResetGrid();
        }


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
                _ => 0,
            }];
        }


        private Image[] SetGridLayers(int n)
        {
            var images = new Image[n];

            int childNum = transform.childCount;

            var backGround = transform.GetChild(0);

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

            for (int i = n; i < newChildNum; i++) transform.GetChild(i).gameObject.SetActive(false);

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
                var type = TowerInfo.Hais[0].Spec.HaiType;

                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? TowerInfo.Hais[0].Spec.Number + 1
                    : TowerInfo.Hais[0].Spec.Number;
                // grid tower
                var images = count switch
                {
                    1 => SetGridLayers(2),
                    _ => SetGridLayers(3), //2, 3, 4
                };
                images[0].sprite = Tower.SingleMentsuSpriteDict[$"BackgroundHai{count}"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];

                if (count > 1)
                    images[2].sprite = TowerInfo switch
                    {
                        KoutsuInfo koutsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(koutsu.IsMenzen ? 7 : 6)}"],
                        ShuntsuInfo shuntsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(shuntsu.IsMenzen ? 5 : 4)}"],
                        KantsuInfo kantsu => Tower.SingleMentsuSpriteDict[$"Mentsu{(kantsu.IsMenzen ? 3 : 2)}"],
                        _ => Tower.SingleMentsuSpriteDict["Mentsu1"],
                    };
            }
            else if (this is GridCell cell)
            {
                if (TowerInfo is TripleTowerInfo)
                {
                    //TowerOption 중에서 TowerImageOption만 받아오기
                    var towerOptions = cell.Pair.TowerStat.Options;

                    List<TowerImageOption> towerImageOptions = new();

                    foreach (var option in towerOptions.Values)
                        if (option is TowerImageOption)
                            towerImageOptions.Add((TowerImageOption)option);

                    //받아온 TowerImageOption에서 Images 받아와서 imageList에 저장
                    List<(int index, int order)> imagesList = new();

                    foreach (var towerImageOption in towerImageOptions)
                    {
                        var images = towerImageOption.Images;

                        foreach ((int i, int o) in images) imagesList.Add((i, o));
                    }

                    var gridImages = SetGridLayers(imagesList.Count + 1);
                    gridImages[0].sprite = Tower.TripleSpriteList[0];

                    int layerCount = 1;
                    foreach ((int index, int _) in imagesList.OrderBy(x => x.order))
                        gridImages[layerCount++].sprite = Tower.TripleSpriteList[index];
                }
                else if (TowerInfo is CompleteTowerInfo cpl)
                {
                    var imagesList = cell.Pair.TowerStat.Options.Values
                        .Where(x => x is TowerImageOption)
                        .Cast<TowerImageOption>()
                        .SelectMany(x => x.Images)
                        .ToList();

                    var gridImages = SetGridLayers(imagesList.Count + 1);
                    gridImages[0].sprite = Tower.CompleteSpriteList[cpl.YakuList.Count == 0 || !cpl.YakuList[0].IsYakuman ? 0 : 33];

                    int layerCount = 1;
                    foreach ((int index, int _) in imagesList.OrderBy(x => x.order))
                        gridImages[layerCount++].sprite = Tower.CompleteSpriteList[index];
                }
            }
        }
    }

    public enum GridCellState
    {
        Idle,
        Choosable,
        NotChoosable,
        Choosed,
    }
}
