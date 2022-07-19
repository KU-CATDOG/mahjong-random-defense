using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

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
            SetGridRowLimit(2);
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
                for (int n = stCode & 16; n < (edCode & 16); n++)
                {
                    HaiSpec hai = new((HaiType)(t * 10), n);
                    for (int i = 0; i < 4; i++)
                    {
                        haiDeck.Add(new Hai(t << 8 & n << 4 & i, hai));
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
                    break;
                case EditState.Add:
                    break;
            }
        }

        private void ChangeState(EditState nextState)
        {
            switch (nextState)
            {
                case EditState.Idle:
                    ForGridCells(cell => { cell.State = GridCellState.Idle; });
                    break;
            }
            SetButtons(nextState);
            _state = nextState;
            choosedCells.Clear();
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
    public enum EditState { Idle, Add, }
}