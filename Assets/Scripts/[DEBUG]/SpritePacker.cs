using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MRD
{
    public class SpritePacker : MonoBehaviour
    {
        public string inPath;

        [ContextMenu("Pack")]
        private void pack()
        {
            var pngs = Directory.GetFiles(inPath).Where(x => Path.GetExtension(x) == ".png").Select(x => File.ReadAllBytes(x)).ToList();
            var names = Directory.GetFiles(inPath).Where(x => Path.GetExtension(x) == ".png").Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            Texture2D first = new(0, 0);
            first.LoadImage(pngs[0]);

            int col = Enumerable.Range(1, Mathf.CeilToInt(Mathf.Sqrt(pngs.Count))).Select(x => (x, Mathf.Max(x * first.width, first.height * Mathf.CeilToInt(pngs.Count / (float)x)))).OrderBy(x => x.Item2).First().x;
            int row = Mathf.CeilToInt(pngs.Count / (float)col);
            Texture2D packed = new Texture2D(first.width * col, first.height * row);

            for (int i = 0; i < pngs.Count; i++)
            {
                Texture2D tmp = new(0, 0);
                tmp.LoadImage(pngs[i]);
                packed.SetPixels(first.width * (i % col), first.height * (row - 1 - i / col), first.width, first.height, tmp.GetPixels());
            }
            File.WriteAllBytes(Path.Combine(Application.dataPath,  "packed.png"), packed.EncodeToPNG());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath("Assets/packed.png");

            int maxSize = 32;
            for (; maxSize < Mathf.Max(packed.width, packed.height); maxSize *= 2) ;
            importer.maxTextureSize = maxSize;
            importer.textureType = TextureImporterType.Sprite;
            importer.spritesheet = Enumerable.Range(0, pngs.Count).Select(x =>
            {
                var tmp = new SpriteMetaData();
                tmp.rect = new Rect(first.width * (x % col), first.height * (row - 1 - x / col), first.width, first.height);
                tmp.name = names[x];
                return tmp;
            }).ToArray();
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }
    }
}