using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Grid : MonoBehaviour
    {
        private AttackCell[,] cells;
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
        private Vector2 attackCellGap = new Vector2(1.9f, .4f);
        [SerializeField]
        private float attackCellTilt = -.3f;
        [SerializeField]
        private float attackCenterHeight = 1f;
        [Header("GridCell")]
        [SerializeField]
        private Transform gridTransform;
        [SerializeField]
        private GameObject gridCellPrefab;
        [SerializeField]
        private float gridCellSize = 1.8f;
        [SerializeField]
        private float gridCellGap = 1.95f;
        [SerializeField]
        private float gridCenterHeight = 7f;

        #region reset
        public void InitGame()
        {
            cells = new AttackCell[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j] = Instantiate(attackCellPrefab, attackTransform).GetComponent<AttackCell>();
                    cells[i, j].Init(Instantiate(gridCellPrefab, gridTransform).GetComponent<GridCell>(), (i, j));
                }
            }
        }
        public void ResetGame()
        {
            SetGridRowLimit(2);
            gridTransform.gameObject.SetActive(false);
            ResetDeck();
        }

        public void SetGridRowLimit(int rowLimit)
        {
            gridRowLimit = rowLimit;
            attackTransform.position = new Vector3(5f - attackCellTilt * (gridRowLimit - 1) * .5f, attackCenterHeight);
            gridTransform.position = new Vector3(5f, gridCenterHeight - gridCellGap * (gridRowLimit - 1) * .5f);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j].transform.localPosition = new Vector3(j - 2, i) * attackCellGap + i * Vector2.right * attackCellTilt;
                    cells[i, j].GetComponent<SpriteRenderer>().sortingOrder = 6 - i;
                    cells[i, j].Pair.transform.localPosition = new Vector3(j - 2, i) * gridCellGap;
                }
                if (i < gridRowLimit)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        cells[i, j].gameObject.SetActive(true);
                    }
                }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        cells[i, j].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void ResetDeck()
        {
            haiDeck = new();
            int stCode = 0x111;
            int edCode = 0x34AAA;

            for (int t = 0; t < 5; t++)
            {
                for (int n = stCode & 16; n < (edCode & 16); n++)
                {
                    HaiSpec hai = new((HaiType)(t * 10), n);
                    for (int i = 0; i < 4; i++)
                    {
                        haiDeck.Add(new(t << 8 & n << 4 & i, hai));
                    }
                }
                stCode >>= 4;
                edCode >>= 4;
            }
        }
        #endregion

        private Hai TsumoHai()
        {
            int index = Random.Range(0, haiDeck.Count);
            Hai ret = haiDeck[index];
            haiDeck.RemoveAt(index);
            return ret;
        }
    }
}