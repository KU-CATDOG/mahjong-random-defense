using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Grid : MonoBehaviour
    {
        private GridCell[,] cells;
        private int gridRowLimit;
        private List<Hai> haiDeck;
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
            gridTransform = new GameObject().transform;
            cells = new GridCell[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j] = Instantiate(gridCellPrefab, new Vector3(j - 2, i) * gridCellGap, Quaternion.identity, gridTransform).GetComponent<GridCell>();
                }
            }
        }
        public void ResetGame()
        {
            gridRowLimit = 2;
            SetGridPosition();
            ResetDeck();
        }

        public void SetGridPosition()
        {
            gridTransform.position = new Vector3(5f, gridCenterHeight - gridCellGap * (gridRowLimit - 1) * .5f);
            for (int i = 0; i < 5; i++)
            {
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