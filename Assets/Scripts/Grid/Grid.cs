using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Grid
    {
        private GridCell[,] cells;
        private int gridRowLimit;
        private List<Hai> haiDeck;

        #region reset
        public void ResetGame()
        {
            cells = new GridCell[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cells[i, j] = new();
                }
            }
            ResetDeck();
            gridRowLimit = 2;
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