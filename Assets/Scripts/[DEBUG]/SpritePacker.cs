using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class SpritePacker : MonoBehaviour
    {
        public string inPath, outPath;

        [ContextMenu("Pack")]
        private void pack()
        {
            var pngs = Directory.GetFiles(inPath).Where(x => Path.GetExtension(x) == ".png").Select(x => File.ReadAllBytes(x)).ToList();
            Texture2D first = new(0, 0);
            first.LoadImage(pngs[0]);

            int col = Enumerable.Range(1, Mathf.FloorToInt(Mathf.Sqrt(pngs.Count)) - 1).Select(x => (x, Mathf.Max(x * first.width, first.height * Mathf.CeilToInt(pngs.Count / (float)x)))).OrderBy(x => x.Item2).First().x;
            int row = Mathf.CeilToInt(pngs.Count / (float)col);
            Texture2D packed = new Texture2D(first.width * col, first.height * row);

            for (int i = 0; i < pngs.Count; i++)
            {
                Texture2D tmp = new(0, 0);
                tmp.LoadImage(pngs[i]);
                packed.SetPixels(first.width * (i % col), first.height * (row - 1 - i / col), first.width, first.height, tmp.GetPixels());
            }
            File.WriteAllBytes(outPath + "packed.png", packed.EncodeToPNG());
        }
    }
}