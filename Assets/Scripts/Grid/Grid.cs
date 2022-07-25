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
        private RoundManager round => GetComponent<RoundManager>();
        private Tower[,] cells;
        public int gridRowLimit { get; private set; }
        private List<FuroCell> furoCells = new();
        private int gridFuroLimit;
        private List<SingleHaiInfo> haiDeck;
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

        [Header("FuroCell")]
        [SerializeField]
        private GameObject furoCellPrefab;
        [SerializeField]
        private float furoCellSize = 1.8f;
        [SerializeField]
        private float furoCellGap = 1.95f;
        [SerializeField]
        private float furoCellY = 1f;
        private const int maxFuroCell = 3;

        private List<UICell> choosedCells = new();
        [SerializeField]
        private Transform redLine;

        private EditState _state;
        public EditState State
        {
            get => _state;
            set => ChangeState(value);
        }

        private static int[] upgradeCost = new int[] { 40, 60, 80 };
        [SerializeField]
        private int upgradeDescent = 2;
        private int currentUpgrade;
        public int CurrentUpgrade
        {
            get => currentUpgrade;
            set
            {
                currentUpgrade = Mathf.Max(0, value);
                canvas.UpgradeText.text = $"[{currentUpgrade}]";
            }
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
            for (int i = 0; i < maxFuroCell; i++)
            {
                var obj = Instantiate(furoCellPrefab, canvas.GridParent).GetComponent<FuroCell>();
                obj.Rect.sizeDelta = Vector2.one * furoCellSize;
                furoCells.Add(obj);
            }
        }
        public void ResetGame()
        {
            Tower.LoadSprites();
            UICell.LoadSprites();
            SetUICells(rowLimit: 2, furoLimit: 1);
            canvas.BlackScreen.gameObject.SetActive(false);
            ResetDeck();
            CurrentUpgrade = upgradeCost[0];
            canvas.UpgradeButton.AddListenerOnly(() => UpgradeRow());
            State = EditState.Idle;
        }

        public void SetUICells(int? rowLimit = null, int? furoLimit = null)
        {
            gridRowLimit = rowLimit ?? gridRowLimit;
            gridFuroLimit = furoLimit ?? gridFuroLimit;
            attackTransform.position = new Vector3(5f - attackCellTilt * (gridRowLimit - 1) * .5f, attackCenterHeight);
            canvas.GridParent.anchoredPosition = new Vector3(0, -gridCellGap * (gridRowLimit - 1) * .5f + gridCellY);
            for (int i = 0; i < gridRowLimit; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j].transform.localPosition = new Vector3(j - 2, i) * attackCellGap + i * Vector2.right * attackCellTilt;
                    cells[i, j].GetComponent<SpriteRenderer>().sortingOrder = 6 - i;
                    cells[i, j].Pair.Rect.anchoredPosition = new Vector3(j - 2, i) * gridCellGap;
                    cells[i, j].gameObject.SetActive(true);
                    cells[i, j].Pair.gameObject.SetActive(true);
                }
            }
            for (int i = gridRowLimit; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j].gameObject.SetActive(false);
                    cells[i, j].Pair.gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < gridFuroLimit; i++)
            {
                furoCells[i].gameObject.SetActive(true);
                furoCells[i].Rect.anchoredPosition = new Vector2(2, gridRowLimit) * gridCellGap + new Vector2 (-furoCellGap * i, furoCellY);
            }
            for (int i = gridFuroLimit; i < maxFuroCell; i++)
            {
                furoCells[i].gameObject.SetActive(false);
            }
            redLine.position = new Vector3(0f, 2f + ((gridRowLimit - 1) * 0.4f));
        }

        private void ResetDeck()
        {
            haiDeck = new();
            int stCode = 0x111;
            int edCode = 0x34AAA;

            for (int t = 0; t < 5; t++)
            {
                for (int n = stCode & 0xF; n < (edCode & 0xF); n++)
                {
                    HaiSpec hai = new((HaiType)((t+1) * 10), n);
                    for (int i = 0; i < 4; i++)
                    {
                        haiDeck.Add(new SingleHaiInfo(new Hai(t << 8 | n << 4 | i, hai)));
                    }
                }
                stCode >>= 4;
                edCode >>= 4;
            }
        }
        #endregion

        public void DescentUpgrade()
        {
            CurrentUpgrade -= upgradeDescent;
        }
        public void UpgradeRow()
        {
            if (round.MinusTsumoToken(CurrentUpgrade))
            {
                SetUICells(rowLimit: gridRowLimit + 1);
                if (gridRowLimit < 5)
                {
                    CurrentUpgrade = upgradeCost[gridRowLimit - 2];
                }
                else
                {
                    canvas.UpgradeButton.gameObject.SetActive(false);
                }
            }
        }

        private void SetButtons() => SetButtons(State);

        
        private void SetButtons(EditState nextState)
        {
            switch (nextState)
            {
                case EditState.Idle:
                    canvas.ChangeButtonImage(0, 3);
                    canvas.ChangeButtonImage(1, 4);
                    canvas.ChangeButtonImage(2, 2);
                    canvas.Buttons[1].AddListenerOnly(() => { if (round.tsumoToken > 0) State = EditState.Add; });
                    canvas.Buttons[0].AddListenerOnly(() => State = EditState.Join);
                    canvas.Buttons[2].AddListenerOnly(() => State = EditState.DelMov);
                    break;

                case EditState.Add:
                    canvas.ChangeButtonImage(1, 0);
                    canvas.Buttons[1].AddListenerOnly(() => State = EditState.Idle);
                    break;

                case EditState.Join:
                    canvas.ChangeButtonImage(0, 0);
                    canvas.ChangeButtonImage(1, 1);
                    canvas.ChangeButtonImage(2, 2);
                    canvas.Buttons[0].AddListenerOnly(() => State = EditState.Idle);
                    canvas.Buttons[1].AddListenerOnly(() => 
                    {
                        if (JoinTower())
                            State = EditState.Idle;
                    });
                    break;

                case EditState.DelMov:
                    canvas.ChangeButtonImage(2, 0);
                    canvas.ChangeButtonImage(1, 1);
                    canvas.ChangeButtonImage(0, 3);
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
                    for(int i = 0; i < gridFuroLimit; i++) furoCells[i].State = GridCellState.Idle;
                    SetTowerImage();
                    break;

                case EditState.Add:
                    ForGridCells(cell =>
                    {
                        if (cell.Pair.TowerStat.TowerInfo == null) cell.State = GridCellState.Choosable;
                        else cell.State = GridCellState.NotChoosable;
                    });
                    break;

                case EditState.Join:
                    canvas.Buttons[2].ClearListener();
                    EnableJoinCandidates();
                    break;

                case EditState.DelMov:
                    canvas.Buttons[0].ClearListener();
                    EnableMoveDelete();
                    break;

            }
            SetButtons(nextState);
            _state = nextState;
            choosedCells.Clear();
        }

        private void EnableMoveDelete()
        {
            if (choosedCells.Count == 0)
            {
                ForGridCells(cell =>
                {
                    if (cell.Pair.TowerStat.TowerInfo != null)
                        cell.State = GridCellState.Choosable;
                });
            }
            else
            {
                ForGridCells(cell =>
                {
                    if (cell.State != GridCellState.Choosed)
                        cell.State = GridCellState.Choosable;
                });
            }
        }

        private bool DeleteTower()
        {
            if (choosedCells.Count == 0 || choosedCells[0] is not GridCell cell)
                return false;

            if (cell.TowerInfo is MentsuInfo or SingleHaiInfo)
                BackHais(cell.TowerInfo);

            round.PlusTsumoToken(cell.TowerInfo.Hais.Count - 1);
            cell.Pair.SetTower(null);
            FillHuroCell();

            return true;
        }

        private void MoveTower()
        {
            if (choosedCells.Count == 0 || choosedCells[0] is not GridCell from || choosedCells[1] is not GridCell to) return;
            var tmp = to.TowerInfo;
            to.Pair.SetTower(from.TowerInfo);
            from.Pair.SetTower(tmp);
        }



        private void EnableJoinCandidates()
        {
            List<TowerInfo> item = new();

            ForGridCells(cell =>
            {
                if (cell.Pair.TowerStat.TowerInfo != null)
                    item.Add(cell.TowerInfo);
            });

            List<TowerInfo> selected = choosedCells.Select(x => x.TowerInfo).ToList();
            List<JoinResult> candidate;
            if (choosedCells.Any(x => x is FuroCell))
            {
                candidate = TowerInfoJoiner.Instance.GetAllPossibleSets(item.Union(selected).ToList(), selected);
                ForGridCells(cell =>
                {
                    var info = cell.Pair.TowerStat.TowerInfo;
                    if (cell.State != GridCellState.Choosed)
                        cell.State = (info != null && candidate.Any(x => x.Candidates.Contains(info))) ? GridCellState.Choosable : GridCellState.NotChoosable;
                });
            }
            else
            {
                candidate = new();
                candidate.AddRange(TowerInfoJoiner.Instance.GetAllPossibleSets(item, selected));
                for (int i = 0; i < gridFuroLimit; i++)
                {
                    if (furoCells[i].TowerInfo != null)
                    {
                        candidate.AddRange(TowerInfoJoiner.Instance.GetAllPossibleSets(item.Append(furoCells[i].TowerInfo).ToList(), selected.Append(furoCells[i].TowerInfo).ToList()));
                    }
                }
            }

            ForGridCells(cell =>
            {
                if (cell.State != GridCellState.Choosed)
                    cell.State = (cell.TowerInfo != null && candidate.Any(x => x.Candidates.Contains(cell.TowerInfo))) ? GridCellState.Choosable : GridCellState.NotChoosable;
            });
            for (int i = 0; i < gridFuroLimit; i++)
            {
                if (furoCells[i].State != GridCellState.Choosed)
                    furoCells[i].State = (furoCells[i].TowerInfo != null && candidate.Any(x => x.Candidates.Contains(furoCells[i].TowerInfo))) ? GridCellState.Choosable : GridCellState.NotChoosable;
            }
        }

        private void SetTowerImage()
        {
            ForGridCells(cell => { cell.Pair.ApplyTowerImage(); cell.ApplyTowerImage(); });
            for (int i = 0; i < gridFuroLimit; i++) furoCells[i].ApplyTowerImage();
        }
        private void BackHais(TowerInfo info)
        {
            foreach (var hai in info.Hais)
            {
                haiDeck.Add(new SingleHaiInfo(new Hai(hai.Id, hai.Spec)));
            }
                    
        }
        private bool JoinTower()
        {
            List<TowerInfo> selected = choosedCells.Select(x => x.TowerInfo).ToList();
            var candidate = TowerInfoJoiner.Instance.GetAllPossibleSets(selected, selected);

            if (candidate.Count == 0)
                return false;

            GridCell target = null;
            foreach (var cell in choosedCells)
            {
                if (cell is GridCell gridCell)
                {
                    target = gridCell;
                    break;
                }
            }
            if (target == null) return false;

            TowerInfo result = candidate.First().Generate();

            if (result is TripleTowerInfo or CompleteTowerInfo)
            {
                foreach (var cell in choosedCells)
                {
                    if (cell.TowerInfo is MentsuInfo mentsu)
                        BackHais(mentsu);
                }
            }
            target.Pair.SetTower(result);
            
            for(int i = 0; i < choosedCells.Count; i++)
            {
                if (choosedCells[i] == target) continue;
                switch (choosedCells[i])
                {
                    case GridCell gridCell:
                        gridCell.Pair.SetTower(null);
                        break;
                    case FuroCell huroCell:
                        huroCell.SetTowerInfo(null);
                        break;
                }
            }
            FillHuroCell();
            return true;
        }
        private void FillHuroCell()
        {
            for (int i = 0; i < gridFuroLimit; i++)
            {
                if (furoCells[i].TowerInfo != null)
                {
                    furoCells[i].TowerInfo.Hais[0].IsFuroHai = false;
                    haiDeck.Add((SingleHaiInfo)furoCells[i].TowerInfo);
                }
            }
            float[] probability = new float[] { .3f, .5f, .6f };
            List<TowerInfo> gridInfos = new();
            ForGridCells(cell =>
            {
                if (cell.TowerInfo is SingleHaiInfo)
                {
                    gridInfos.Add(cell.TowerInfo);
                }
            });
            IEnumerable<TowerInfo>[] furoLists = new IEnumerable<TowerInfo>[3]; //chi, peng, kang
            furoLists[0] = haiDeck.Where(x => TowerInfoJoiner.Instance.CheckTowerJoinable(gridInfos.Append(x).ToList(), x, new ShuntsuJoiner()));
            furoLists[1] = haiDeck.Where(x => TowerInfoJoiner.Instance.CheckTowerJoinable(gridInfos.Append(x).ToList(), x, new KoutsuJoiner()));
            furoLists[2] = haiDeck.Where(x => TowerInfoJoiner.Instance.CheckTowerJoinable(gridInfos.Append(x).ToList(), x, new KantsuJoiner()));
            for (int i = 0; i < gridFuroLimit; i++)
            {
                TowerInfo picked = null;
                var rnd = UnityEngine.Random.Range(0f, 1f);
                int count;
                for (int j = 0; j < 3; j++)
                {
                    if (rnd < probability[j])
                    {
                        count = furoLists[j].Count();
                        if (count > 0)
                        {
                            picked = furoLists[j].ElementAt(UnityEngine.Random.Range(0, count));
                        }
                        break;
                    }
                }
                if (picked != null)
                {
                    picked.Hais[0].IsFuroHai = true;
                    for (int j = 0; j < 3; j++)
                    {
                        furoLists[j] = furoLists[i].Where(x => x.Hais[0].Spec != picked.Hais[0].Spec);
                    }
                    haiDeck.Remove((SingleHaiInfo)picked);
                }
                furoCells[i].SetTowerInfo((SingleHaiInfo)picked);
            }
        }


        public void SelectCell(UICell cell)
        {
            if (choosedCells.Contains(cell)) return;            
            choosedCells.Add(cell);
            
            switch (State)
            {
                case EditState.Add:
                    if(choosedCells.Count > 0)
                    {
                        ((GridCell)choosedCells[0]).Pair.SetTower(TsumoHai());
                        FillHuroCell();
                        round.MinusTsumoToken(1);
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

        public void DeselectCell(UICell cell)
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

        private SingleHaiInfo TsumoHai()
        {
            int index = UnityEngine.Random.Range(0, haiDeck.Count);
            // while(haiDeck[index].Hai.Spec.Number != 9) index = UnityEngine.Random.Range(0, haiDeck.Count);
            var ret = haiDeck[index];
            haiDeck.RemoveAt(index);
            return ret;
        }

    }
    public enum EditState { Idle, Add, Join, DelMov }
}