using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MRD
{
    public class Grid : MonoBehaviour
    {
        private const int maxFuroCell = 3;

        private static readonly int[] upgradeCost = { 30, 60, 90 };

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

        [SerializeField]
        private TowerStatImageController towerStatImageController;

        [Header("FuroCell")]
        [SerializeField]
        private GameObject furoCellPrefab;

        [SerializeField]
        private float furoCellSize = 1.8f;

        [SerializeField]
        private float furoCellGap = 1.95f;

        [SerializeField]
        private float furoCellY = 1f;

        [SerializeField]
        private Transform redLine;
        [HideInInspector]
        public float RedLineY => redLine.position.y;

        [SerializeField]
        private int upgradeDescent = 2;

        private EditState _state;
        private Tower[,] cells;

        private readonly List<UICell> choosedCells = new();
        private int currentUpgrade;
        private readonly List<FuroCell> furoCells = new();
        private int gridFuroLimit;
        private List<SingleHaiInfo> haiDeck;
        private RoundManager round => GetComponent<RoundManager>();
        public int gridRowLimit { get; private set; }
        private bool[] rowLocked = new bool[5]{false, false, false, false, false};
        public GameObject JoinAnimatorPrefab;

        public EditState State
        {
            get => _state;
            set => ChangeState(value);
        }

        public int CurrentUpgrade
        {
            get => currentUpgrade;
            set
            {
                currentUpgrade = Mathf.Max(0, value);
                canvas.UpgradeText.text = $"[{currentUpgrade}]";
            }
        }

        public void DescentUpgrade()
        {
            CurrentUpgrade -= upgradeDescent;
        }

        public void UpgradeRow()
        {
            if (round.MinusTsumoToken(CurrentUpgrade))
            {
                SetUICells(gridRowLimit + 1);
                if (gridRowLimit < 5)
                    CurrentUpgrade = upgradeCost[gridRowLimit - 2];
                else
                    canvas.UpgradeButton.gameObject.SetActive(false);
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
                    //canvas.ChangeButtonImage(2, 2);
                    canvas.Buttons[1].AddListenerOnly(() =>
                    {
                        if (round.tsumoToken <= 0) return;

                        var empty = cells.Cast<Tower>().Where(x => x.TowerStat.TowerInfo == null && x.Coordinate.X < gridRowLimit).ToList();
                        if (empty.Count == 0) return;

                        empty[Random.Range(0, empty.Count)].SetTower(TsumoHai());
                        FillFuroCell(false);
                        round.MinusTsumoToken(1);
                        State = EditState.Idle;
                    });
                    canvas.Buttons[0].AddListenerOnly(() =>
                    {
                        ForGridCells(cells => cells.State = GridCellState.Idle);
                        State = EditState.Join;
                    });
                    canvas.ResetButton.AddListenerOnly(() =>
                    {
                        ResetGrid();
                    });
                    //canvas.Buttons[2].AddListenerOnly(() => State = EditState.DelMov);
                    break;

                case EditState.Add:
                    canvas.ChangeButtonImage(1, 0);
                    canvas.Buttons[1].AddListenerOnly(() => State = EditState.Idle);
                    break;

                case EditState.Join:
                    canvas.ChangeButtonImage(0, 0);
                    canvas.ChangeButtonImage(1, 1);
                    //canvas.ChangeButtonImage(2, 2);
                    canvas.Buttons[0].AddListenerOnly(() => State = EditState.Idle);
                    canvas.Buttons[1].AddListenerOnly(() =>
                    {
                        if (JoinTower())
                            State = EditState.Idle;
                    });
                    canvas.ResetButton.AddListenerOnly(() =>
                    {
                        ResetGrid();
                    });
                    break;

                /*case EditState.DelMov:
                    canvas.ChangeButtonImage(2, 0);
                    canvas.ChangeButtonImage(1, 1);
                    canvas.ChangeButtonImage(0, 3);
                    canvas.Buttons[2].AddListenerOnly(() => State = EditState.Idle);
                    canvas.Buttons[1].AddListenerOnly(() => 
                    {
                        if (DeleteTower())
                            State = EditState.Idle;
                    });
                    break;*/
            }
        }

        private void ChangeState(EditState nextState)
        {
            switch (nextState)
            {
                case EditState.Idle:
                    ForGridCells(cell => { cell.State = GridCellState.Idle; });
                    for (int i = 0; i < gridFuroLimit; i++) furoCells[i].State = GridCellState.Idle;
                    SetTowerImage();
                    break;

                case EditState.Add:
                    RemoveTowerStatImage();
                    ForGridCells(cell =>
                    {
                        if (cell.Pair.TowerStat.TowerInfo == null) cell.State = GridCellState.Choosable;
                        else cell.State = GridCellState.NotChoosable;
                    });
                    break;

                case EditState.Join:
                    RemoveTowerStatImage();
                    //canvas.Buttons[2].ClearListener();
                    EnableJoinCandidates();
                    break;

                /*case EditState.DelMov:
                    canvas.Buttons[0].ClearListener();
                    EnableMoveDelete();
                    break;*/
            }

            SetButtons(nextState);
            _state = nextState;
            choosedCells.Clear();
        }

        public void ResetGrid()
        {
            ForGridCells(cells =>
            {
                if(State == EditState.Idle)
                {
                    cells.State = GridCellState.Idle;
                    DeselectCell(cells);
                    RemoveTowerStatImage();
                }
                else 
                {
                    if (!choosedCells.Contains(cells))
                    {
                        cells.State = cells.State is GridCellState.Choosable ? GridCellState.Choosable : GridCellState.NotChoosable;
                        RemoveTowerStatImage();
                    }
                    else
                    {
                        cells.State = GridCellState.Choosable;
                        DeselectCell(cells);
                        RemoveTowerStatImage();
                    }
                }
                
            });
            for (int i = 0; i < gridFuroLimit; i++)
            {
                if (furoCells[i].State is GridCellState.Choosed)
                {
                    furoCells[i].State = GridCellState.Choosable;
                    DeselectCell(furoCells[i]);
                }
            }
        }
        public void SetTrashCan(bool key)
        {
            canvas.TrashCan.SetActive(key);
        }

        public void SetTowerStatImage(GridCell cell)
        {
            towerStatImageController.ShowTowerStat(cell.Pair.TowerStat);
        }

        public void RemoveTowerStatImage()
        {
            towerStatImageController.RemoveTowerStat();
        }

        /*private void EnableMoveDelete()
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
        }*/

        /* private bool DeleteTower()
         {
             if (choosedCells.Count == 0 || choosedCells[0] is not GridCell cell)
                 return false;
 
             if (cell.TowerInfo is MentsuInfo or SingleHaiInfo)
                 BackHais(cell.TowerInfo);
 
             round.PlusTsumoToken(cell.TowerInfo.Hais.Count - 1);
             cell.Pair.SetTower(null);
             FillHuroCell();
 
             return true;
         }*/

        /*private void MoveTower()
        {
            if (choosedCells.Count == 0 || choosedCells[0] is not GridCell from || choosedCells[1] is not GridCell to) return;
            var tmp = to.TowerInfo;
            to.Pair.SetTower(from.TowerInfo);
            from.Pair.SetTower(tmp);
        }*/


        private void EnableJoinCandidates()
        {
            List<TowerInfo> item = new();

            ForGridCells(cell =>
            {
                if (cell.Pair.TowerStat.TowerInfo != null)
                    item.Add(cell.TowerInfo);
            });

            var selected = choosedCells.Select(x => x.TowerInfo).ToList();
            List<JoinResult> candidate;
            if (choosedCells.Any(x => x is FuroCell))
            {
                candidate = TowerInfoJoiner.Instance.GetAllPossibleSets(item.Union(selected).ToList(), selected);
                ForGridCells(cell =>
                {
                    var info = cell.Pair.TowerStat.TowerInfo;
                    if (cell.State != GridCellState.Choosed)
                        cell.State = info != null && candidate.Any(x => x.Candidates.Contains(info))
                            ? GridCellState.Choosable
                            : GridCellState.NotChoosable;
                });
            }
            else
            {
                candidate = new List<JoinResult>();
                candidate.AddRange(TowerInfoJoiner.Instance.GetAllPossibleSets(item, selected));
                for (int i = 0; i < gridFuroLimit; i++)
                    if (furoCells[i].TowerInfo != null)
                        candidate.AddRange(TowerInfoJoiner.Instance.GetAllPossibleSets(
                            item.Append(furoCells[i].TowerInfo).ToList(),
                            selected.Append(furoCells[i].TowerInfo).ToList()));
            }

            ForGridCells(cell =>
            {
                if (cell.State != GridCellState.Choosed)
                    cell.State = cell.TowerInfo != null && candidate.Any(x => x.Candidates.Contains(cell.TowerInfo))
                        ? GridCellState.Choosable
                        : GridCellState.NotChoosable;
            });
            for (int i = 0; i < gridFuroLimit; i++)
            {
                if (furoCells[i].State != GridCellState.Choosed)
                    furoCells[i].State =
                        furoCells[i].TowerInfo != null &&
                        candidate.Any(x => x.Candidates.Contains(furoCells[i].TowerInfo))
                            ? GridCellState.Choosable
                            : GridCellState.NotChoosable;
            }
        }

        private void SetTowerImage()
        {
            ForGridCells(cell =>
            {
                cell.Pair.ApplyTowerImage();
                cell.ApplyTowerImage();
            });
            for (int i = 0; i < gridFuroLimit; i++) furoCells[i].ApplyTowerImage();
        }

        private void BackHais(TowerInfo info)
        {
            foreach (var hai in info.Hais) haiDeck.Add(new SingleHaiInfo(new Hai(hai.Id, hai.Spec)));
        }

        private bool JoinTower()
        {
            var selected = choosedCells.Select(x => x.TowerInfo).ToList();
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

            var result = candidate.First().Generate();

            if (result is TripleTowerInfo or CompleteTowerInfo)
                foreach (var cell in choosedCells)
                {
                    if (cell.TowerInfo is MentsuInfo mentsu)
                        BackHais(mentsu);
                }

            target.Pair.SetTower(result);
            StartCoroutine(AnimatorTestCoroutine(choosedCells.Select(x=>x.gameObject).ToList(), target.gameObject));

            for (int i = 0; i < choosedCells.Count; i++)
            {
                if (choosedCells[i] == target) continue;
                switch (choosedCells[i])
                {
                    case GridCell gridCell:
                        gridCell.Pair.SetTower(null);
                        break;
                    case FuroCell furoCell:
                        furoCell.SetTowerInfo(null);
                        break;
                }
            }

            FillFuroCell(true);
            return true;
        }

        private void FillFuroCell(bool refillAll)
        {
            List<HaiSpec> onSpec = new();
            List<FuroCell> refillCells = new();
            List<TowerInfo> gridInfos = cells.Cast<Tower>().Where(x => x.Pair.TowerInfo is SingleHaiInfo).Select(x => x.Pair.TowerInfo).ToList();
            ITowerInfoJoiner[] furoJoiners = new ITowerInfoJoiner[] { new ShuntsuJoiner(), new KoutsuJoiner(), new KantsuJoiner() };
            for (int i = 0; i < gridFuroLimit; i++)
            {
                if (furoCells[i].TowerInfo == null || refillAll || !TowerInfoJoiner.Instance.CheckTowerJoinable(gridInfos.Append(furoCells[i].TowerInfo).ToList(), furoCells[i].TowerInfo, furoJoiners))
                {
                    refillCells.Add(furoCells[i]);
                }
                else if (furoCells[i].TowerInfo != null)
                {
                    onSpec.Add(furoCells[i].TowerInfo.Hais[0].Spec);
                }
            }
            
            foreach (var cell in refillCells)
            {
                if (cell.TowerInfo != null)
                {
                    cell.TowerInfo.Hais[0].IsFuroHai = false;
                    haiDeck.Add((SingleHaiInfo)cell.TowerInfo);
                }
                List<SingleHaiInfo> triedCell = new();
                SingleHaiInfo picked = null;
                for (int i = 0; i < 10 && haiDeck.Count > 0; i++)
                {
                    picked = TsumoHai();
                    picked.Hai.IsFuroHai = true;
                    if (TowerInfoJoiner.Instance.CheckTowerJoinable(gridInfos.Append(picked).ToList(), picked, furoJoiners) && onSpec.All(x => !x.Equals(picked.Hai.Spec))) break;
                    else
                    {
                        picked.Hai.IsFuroHai = false;
                        triedCell.Add(picked);
                        picked = null;
                    }
                }
                cell.SetTowerInfo(picked);
                cell.ApplyTowerImage();
                haiDeck.AddRange(triedCell);
                if (picked != null) onSpec.Add(picked.Hai.Spec);
            }
        }

        public void SelectCell(UICell cell)
        {
            if (choosedCells.Contains(cell)) return;
            choosedCells.Add(cell);

            //ResetSiblingIndex();

            switch (State)
            {
                case EditState.Idle:
                    if (choosedCells.Count > 1)
                    {
                        choosedCells[0].State = GridCellState.Idle;
                        choosedCells.Clear();
                        choosedCells.Add(cell);
                    }

                    break;

                case EditState.Join:
                    EnableJoinCandidates();
                    break;

                /*case EditState.DelMov:
                    EnableMoveDelete();
                    if (choosedCells.Count > 1)
                    {
                        MoveTower();
                        State = EditState.Idle;
                    }
                    break;*/
            }
        }

        public void DeselectCell(UICell cell)
        {
            if (!choosedCells.Contains(cell)) return;
            choosedCells.Remove(cell);

            //ResetSiblingIndex();


            switch (State)
            {
                case EditState.Join:
                    EnableJoinCandidates();
                    break;

                /* case EditState.DelMov:
                     EnableMoveDelete();
                     break;*/
            }
        }

        private SingleHaiInfo TsumoHai()
        {
            //DEBUG
            if (round.HAI_CHEAT)
            {
                var match = haiDeck.FirstOrDefault(x => x.Hai.Spec.Equals(round.HAI_CHEAT_SPEC_TYPE, round.HAI_CHEAT_SPEC_NUM));
                if (match != null)
                {
                    haiDeck.Remove(match);
                    return match;
                }
                else
                {
                    match = haiDeck.FirstOrDefault(x => x.Hai.Spec.HaiType == round.HAI_CHEAT_SPEC_TYPE);
                    if (match != null)
                    {
                        haiDeck.Remove(match);
                        return match;
                    }
                }
            }
            int index = Random.Range(0, haiDeck.Count);
            var ret = haiDeck[index];
            haiDeck.RemoveAt(index);
            return ret;
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
                    cells[i, j].Init(Instantiate(gridCellPrefab, canvas.GridParent).GetComponent<GridCell>(), (i, j),
                        null);
                    cells[i, j].transform.localScale = Vector3.one * attackCellSize;
                    cells[i, j].Pair.Rect.sizeDelta = Vector2.one * gridCellSize;
                }
            }

            for (int i = 0; i < maxFuroCell; i++)

            {
                var obj = Instantiate(furoCellPrefab, canvas.FuroParent).GetComponent<FuroCell>();

                obj.Rect.sizeDelta = Vector2.one * furoCellSize;

                furoCells.Add(obj);
            }

            ResetSiblingIndex();
        }

        public void ResetSiblingIndex()
        {
            ForGridCells(cell => cell.transform.SetSiblingIndex(0));
        }

        public void ResetGame()
        {
            Tower.LoadSprites();
            UICell.LoadSprites();
            SetUICells(2, 1);
            //DEBUG
            if (round.MONEY_CHEAT) SetUICells(5, 3);
            canvas.BlackScreen.gameObject.SetActive(false);
            ResetDeck();
            CurrentUpgrade = upgradeCost[0];
            canvas.UpgradeButton.AddListenerOnly(() => UpgradeRow());
            State = EditState.Idle;
        }

        public void SetUICells(int? rowLimit = null, int? furoLimit = null)
        {
            bool isRowChange = rowLimit != null && rowLimit != gridRowLimit;
            gridRowLimit = rowLimit ?? gridRowLimit;
            gridFuroLimit = furoLimit ?? gridFuroLimit;
            attackTransform.position = new Vector3(5f - attackCellTilt * (gridRowLimit - 1) * .5f, attackCenterHeight);
            canvas.GridParent.anchoredPosition = new Vector3(0, -gridCellGap * (gridRowLimit - 1) * .5f + gridCellY);
            canvas.FuroParent.anchoredPosition = new Vector3(0, -gridCellGap * (gridRowLimit - 1) * .5f + gridCellY);
            for (int i = 0; i < gridRowLimit; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j].transform.localPosition =
                        new Vector3(j - 2, i) * attackCellGap + i * Vector2.right * attackCellTilt;
                    cells[i, j].GetComponent<SpriteRenderer>().sortingOrder = 6 - i;
                    cells[i, j].Pair.Rect.anchoredPosition = new Vector3(j - 2, i) * gridCellGap;
                    cells[i, j].gameObject.SetActive(true);
                    cells[i, j].Pair.gameObject.SetActive(true);
                    if(i == gridRowLimit - 1)
                        cells[i, j].Pair.Locked = true;
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
                furoCells[i].Rect.anchoredPosition = new Vector2(2, gridRowLimit) * gridCellGap +
                                                     new Vector2(-furoCellGap * i, furoCellY);
            }

            for (int i = gridFuroLimit; i < maxFuroCell; i++) furoCells[i].gameObject.SetActive(false);
            redLine.position = new Vector3(0f, 2f + (gridRowLimit - 1) * 0.4f);
        }

        private void ResetDeck()
        {
            haiDeck = new List<SingleHaiInfo>();
            int stCode = 0x111;
            int edCode = 0x34AAA;

            for (int t = 0; t < 5; t++)
            {
                for (int n = stCode & 0xF; n < (edCode & 0xF); n++)
                {
                    HaiSpec hai = new((HaiType)((t + 1) * 10), n);
                    for (int i = 0; i < 4; i++) haiDeck.Add(new SingleHaiInfo(new Hai((t << 8) | (n << 4) | i, hai)));
                }

                stCode >>= 4;
                edCode >>= 4;
            }
        }

        #endregion

        #region ForEach

        private void ForGridCells(Action<GridCell> action)
        {
            for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                action(cells[i, j].Pair);
        }

        private void ForGridCells(Action<GridCell, int, int> action)
        {
            for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                action(cells[i, j].Pair, i, j);
        }

        #endregion
        [ContextMenu("AnimatorTest")]
        private void AnimatorTest()
        {
            StartCoroutine(AnimatorTestCoroutine(new List<GameObject>{cells[4,0].Pair.gameObject, cells[4,2].Pair.gameObject, cells[4,4].Pair.gameObject},cells[0,2].Pair.gameObject));
        }
        private System.Collections.IEnumerator AnimatorTestCoroutine(List<GameObject> sourceList, GameObject target)
        {
            var newObject = Instantiate(JoinAnimatorPrefab);
            newObject.transform.SetParent(canvas.JoinAnimator,false);
            newObject.GetComponent<JoinAnimator>().Init(sourceList,target);
            yield return null;
        }
    }

    public enum EditState
    {
        Idle,
        Add,
        Join, /*DelMov*/
    }
}
