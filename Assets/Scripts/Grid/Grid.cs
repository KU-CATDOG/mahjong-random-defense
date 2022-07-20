using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;

namespace MRD
{
    public class Grid : MonoBehaviour
    {
        private Tower[,] cells;
        private int gridRowLimit;
        private List<Hai> haiDeck;
        [Header("AttackCell")]
        [SerializeField]
        private Transform attackTransform;
        [SerializeField]
        private GameObject attackCellPrefab;
        [SerializeField]
        private float attackCellSize = 1.6f;
        [SerializeField]
        private Vector2 attackCellGap = new(1.9f, .4f);
        [SerializeField]
        private float attackCellTilt = -.3f;
        [SerializeField]
        private float attackCenterHeight = 1f;
        [Header("GridCell")]

        [SerializeField]
        private CanvasComponents canvas;
        [SerializeField]
        private GameObject gridCellPrefab;
        [SerializeField]
        private float gridCellSize = 1.8f;
        [SerializeField]
        private float gridCellGap = 1.95f;
        [SerializeField]
        private float gridCellY = 1f;

        private List<GridCell> choosedCells = new();

        private EditState _state;
        public EditState State
        {
            get => _state;
            set => ChangeState(value);
        }

        #region reset
        public void InitGame()
        {
            cells = new Tower[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j] = Instantiate(attackCellPrefab, attackTransform).GetComponent<Tower>();
                    cells[i, j].Init(Instantiate(gridCellPrefab, canvas.GridParent).GetComponent<GridCell>(), (i, j), null);
                    cells[i, j].transform.localScale = Vector3.one * attackCellSize;
                    cells[i, j].Pair.Rect.sizeDelta = Vector2.one * gridCellSize;
                }
            }
        }
        public void ResetGame()
        {
            SetGridRowLimit(4);
            canvas.BlackScreen.gameObject.SetActive(false);
            ResetDeck();
            State = EditState.Idle;
        }

        public void SetGridRowLimit(int rowLimit)
        {
            gridRowLimit = rowLimit;
            attackTransform.position = new Vector3(5f - attackCellTilt * (gridRowLimit - 1) * .5f, attackCenterHeight);
            canvas.GridParent.anchoredPosition = new Vector3(0, -gridCellGap * (gridRowLimit - 1) * .5f + gridCellY);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j].transform.localPosition = new Vector3(j - 2, i) * attackCellGap + i * Vector2.right * attackCellTilt;
                    cells[i, j].GetComponent<SpriteRenderer>().sortingOrder = 6 - i;
                    cells[i, j].Pair.Rect.anchoredPosition = new Vector3(j - 2, i) * gridCellGap;
                }
                if (i < gridRowLimit)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        cells[i, j].gameObject.SetActive(true);
                        cells[i, j].Pair.gameObject.SetActive(true);
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        cells[i, j].gameObject.SetActive(false);
                        cells[i, j].Pair.gameObject.SetActive(false);
                    }
                }
            }
        }

        private void ResetDeck()
        {
            haiDeck = new List<Hai>();
            int stCode = 0x111;
            int edCode = 0x34AAA;

            for (int t = 0; t < 5; t++)
            {
                for (int n = stCode & 0xF; n < (edCode & 0xF); n++)
                {
                    HaiSpec hai = new((HaiType)((t+1) * 10), n);
                    for (int i = 0; i < 4; i++)
                    {
                        haiDeck.Add(new Hai(t << 8 | n << 4 | i, hai));
                    }
                }
                stCode >>= 4;
                edCode >>= 4;
            }
        }
        #endregion

        private void SetButtons() => SetButtons(State);

        
        private void SetButtons(EditState nextState)
        {
            switch (nextState)
            {
                case EditState.Idle:
                    canvas.Buttons[1].AddListenerOnly(() => State = EditState.Add);
                    canvas.Buttons[0].AddListenerOnly(() => State = EditState.Join);
                    canvas.Buttons[2].AddListenerOnly(() => State = EditState.DelMov);
                    break;

                case EditState.Add:
                    canvas.Buttons[1].AddListenerOnly(() => State = EditState.Idle);
                    break;

                case EditState.Join:
                    canvas.Buttons[0].AddListenerOnly(() => State = EditState.Idle);
                    canvas.Buttons[1].AddListenerOnly(() => 
                    {
                        if (JoinTower())
                            State = EditState.Idle;
                    });
                    break;

                case EditState.DelMov:
                    canvas.Buttons[2].AddListenerOnly(() => State = EditState.Idle);
                    canvas.Buttons[1].AddListenerOnly(() => 
                    {
                        if (DeleteTower())
                            State = EditState.Idle;
                    });
                    break;
            }
        }

        private void ChangeState(EditState nextState)
        {
            switch (nextState)
            {
                case EditState.Idle:
                    ForGridCells(cell => { cell.State = GridCellState.Idle; });
                    SetTowerImage();
                    break;

                case EditState.Add:                    
                    ForGridCells(cell => 
                    {
                        if (cell.Pair.TowerStat.TowerInfo == null) cell.State = GridCellState.Choosable;
                        else cell.State = GridCellState.NotChoosable; ;
                    });            
                    break;

                case EditState.Join:
                    EnableJoinCandidates();
                    break;

                case EditState.DelMov:
                    EnableMoveDelete();
                    break;

            }
            SetButtons(nextState);
            _state = nextState;
            choosedCells.Clear();
        }

        private void EnableMoveDelete()
        {            
            ForGridCells(cell => 
            {
                var info = cell.Pair.TowerStat.TowerInfo;
                if(cell.State != GridCellState.Choosed)
                    cell.State = info != null ? GridCellState.Choosable : GridCellState.NotChoosable;

                if(choosedCells.Count > 0)
                    ForGridCells(cell =>
                    {
                        var info = cell.Pair.TowerStat.TowerInfo;
                        if (cell.State != GridCellState.Choosed)
                            cell.State = info == null ? GridCellState.Choosable : GridCellState.NotChoosable;
                        else
                            cell.State = GridCellState.Choosed;
                    });
            });



        }

        private bool DeleteTower()
        {
            if (choosedCells.Count == 0)
                return false;

            if (choosedCells[0].Pair.TowerStat.TowerInfo is MentsuInfo or SingleHaiInfo)
                for (int i = 0; i < choosedCells[0].Pair.TowerStat.TowerInfo.Hais.Count; i++)
                    haiDeck.Add(choosedCells[0].Pair.TowerStat.TowerInfo.Hais[i]);

            choosedCells[0].Pair.SetTower(null);

            return true;
        }

        private void MoveTower()
        {
            choosedCells[1].Pair.SetTower(choosedCells[0].Pair.TowerStat.TowerInfo);
            choosedCells[0].Pair.SetTower(null);
        }



        private void EnableJoinCandidates()
        {
            List<TowerInfo> item = new();

            ForGridCells(cell =>
            {
                if (cell.Pair.TowerStat.TowerInfo != null)
                    item.Add(cell.Pair.TowerStat.TowerInfo);
            });

            List<TowerInfo> selected = choosedCells.Select(x => x.Pair.TowerStat.TowerInfo).ToList();

            var candidate = TowerInfoJoiner.Instance.GetAllPossibleSets(item, selected);

            ForGridCells(cell =>
            {
                var info = cell.Pair.TowerStat.TowerInfo;
                if(cell.State != GridCellState.Choosed)
                    cell.State =  (info != null && candidate.Any(x => x.Candidates.Contains(info))) ? GridCellState.Choosable : GridCellState.NotChoosable;
            });
        }

        private void SetTowerImage()
        {
            ForGridCells(cell => cell.Pair.ApplyTowerImage());
        }

        private bool JoinTower()
        {
            List<TowerInfo> selected = choosedCells.Select(x => x.Pair.TowerStat.TowerInfo).ToList();
            var candidate = TowerInfoJoiner.Instance.GetAllPossibleSets(selected, selected);

            if (candidate.Count == 0)
                return false;

            var temp = choosedCells[0].Pair.TowerStat.TowerInfo;
            choosedCells[0].Pair.SetTower(candidate.First().Generate());
            
            for(int i = 1; i< choosedCells.Count; i++)
            {
                if (choosedCells[0].Pair.TowerStat.TowerInfo is TripleTowerInfo)
                {
                    for (int j = 0; j < choosedCells[i].Pair.TowerStat.TowerInfo.Hais.Count; j++)
                        haiDeck.Add(choosedCells[i].Pair.TowerStat.TowerInfo.Hais[j]);

                    for (int j = 0; j < temp.Hais.Count; j++)
                        haiDeck.Add(temp.Hais[j]);                    
                }

                choosedCells[i].Pair.SetTower(null);
            }
            return true;
        }



        public void SelectCell(GridCell cell)
        {
            if (choosedCells.Contains(cell)) return;            
            choosedCells.Add(cell);
            
            switch (State)
            {
                case EditState.Add:
                    if(choosedCells.Count > 0)
                    {
                        choosedCells[0].Pair.SetTower(new SingleHaiInfo(TsumoHai()));
                        State = EditState.Idle;
                    }
                    break;

                case EditState.Join:
                    EnableJoinCandidates();
                    break;

                case EditState.DelMov:
                    EnableMoveDelete();
                    if (choosedCells.Count > 1)
                    {
                        MoveTower();
                        State = EditState.Idle;
                    }
                    break;
            }        

        }

        public void DeselectCell(GridCell cell)
        {
            if (!choosedCells.Contains(cell)) return;
            choosedCells.Remove(cell);
            
            switch (State)
            {
                case EditState.Join:
                    EnableJoinCandidates();
                    break;

                case EditState.DelMov:
                    EnableMoveDelete();
                    break;
            }
        }

        #region ForEach
        private void ForGridCells(Action<GridCell> action)
        {
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    action(cells[i, j].Pair);

                }
            }
        }
        private void ForGridCells(Action<GridCell, int, int> action)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    action(cells[i, j].Pair, i, j);
                }
            }
        }

        #endregion





        private Hai TsumoHai()
        {
            int index = UnityEngine.Random.Range(0, haiDeck.Count);
            Hai ret = haiDeck[index];
            haiDeck.RemoveAt(index);
            return ret;
        }

    }
    public enum EditState { Idle, Add, Join, DelMov}
}