using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace MRD
{
    public class YakuTester : MonoBehaviour
    {
        public HaiType haitype;
        public int number;
        TripleTowerInfo tripleTest;
        CompleteTowerInfo completeTest;
        public string yakuName;

        List<MentsuInfo> mlist = new();
        void Start()
        {

        }

        [ContextMenu("¶ÇÀÌÂê³Ö±â")]
        void makeToitsu()
        {
            mlist.Add(new ToitsuInfo(new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number))));
            hap();
        }
        [ContextMenu("šœÂê³Ö±â")]
        void makeShuntsu()
        {
            mlist.Add(new ShuntsuInfo(new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number + 1)), new Hai(0, new(haitype, number + 2))));
            hap();
        }
        [ContextMenu("Ä¿Âê³Ö±â")]
        void makeKoutsu()
        {
            mlist.Add(new KoutsuInfo(new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number))));
            hap();
        }
        [ContextMenu("±øÂê³Ö±â")]
        void makeKantsu()
        {
            mlist.Add(new KantsuInfo(new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number)), new Hai(0, new(haitype, number))));
            hap();
        }

        void hap()
        {
            if(mlist.Count == 3)
            {
                tripleTest = new(mlist[0], mlist[1], mlist[2]);
            }
            if(mlist.Count == 5)
            {
                completeTest = new(tripleTest, mlist[3], mlist[4]);
            }
            Debug.Log("ÇöÀç:" + mlist.SelectMany(x => x.Hais).Select(x => $"[{x.Spec.HaiType}{x.Spec.Number}]").Aggregate("", (a, b) => a + b));
        }
        [ContextMenu("Å×½ºÆ®")]
        void TestYaku()
        {
            IYakuConditionChecker checker = (IYakuConditionChecker)Activator.CreateInstance(Type.GetType("MRD." + yakuName + "Checker", true));
            YakuHolderInfo x;
            if (tripleTest == null)
            {
                Debug.Log("no yaku tower");
                return;
            }
            else if (completeTest == null)
            {
                Debug.Log("triple tower yaku test");
                x = tripleTest;
            }
            else
            {
                Debug.Log("complete tower yaku test");
                x = completeTest;
            }
            Debug.Log(x.MentsuInfos.SelectMany(x => x.Hais).Select(x => $"[{x.Spec.HaiType}{x.Spec.Number}]").Aggregate("", (a,b)=> a + b));
            Debug.Log(checker.CheckCondition(x) ? yakuName + " Yes" : yakuName + " No");
        }
    }

}