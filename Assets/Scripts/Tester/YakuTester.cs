using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class YakuTester : MonoBehaviour
    {
        public HaiType haitype;
        public int number;
        public string yakuName;
        private CompleteTowerInfo completeTest;

        private readonly List<MentsuInfo> mlist = new();
        private TripleTowerInfo tripleTest;

        private void Start()
        {
        }

        [ContextMenu("������ֱ�")]
        private void makeToitsu()
        {
            mlist.Add(
                new ToitsuInfo(new Hai(0, new HaiSpec(haitype, number)), new Hai(0, new HaiSpec(haitype, number))));
            hap();
        }

        [ContextMenu("����ֱ�")]
        private void makeShuntsu()
        {
            mlist.Add(new ShuntsuInfo(new Hai(0, new HaiSpec(haitype, number)),
                new Hai(0, new HaiSpec(haitype, number + 1)), new Hai(0, new HaiSpec(haitype, number + 2))));
            hap();
        }

        [ContextMenu("Ŀ��ֱ�")]
        private void makeKoutsu()
        {
            mlist.Add(new KoutsuInfo(new Hai(0, new HaiSpec(haitype, number)), new Hai(0, new HaiSpec(haitype, number)),
                new Hai(0, new HaiSpec(haitype, number))));
            hap();
        }

        [ContextMenu("����ֱ�")]
        private void makeKantsu()
        {
            mlist.Add(new KantsuInfo(new Hai(0, new HaiSpec(haitype, number)), new Hai(0, new HaiSpec(haitype, number)),
                new Hai(0, new HaiSpec(haitype, number)), new Hai(0, new HaiSpec(haitype, number))));
            hap();
        }

        private void hap()
        {
            if (mlist.Count == 3) tripleTest = new TripleTowerInfo(mlist[0], mlist[1], mlist[2]);
            if (mlist.Count == 5) completeTest = new CompleteTowerInfo(tripleTest, mlist[3], mlist[4]);
            Debug.Log("����:" + mlist.SelectMany(x => x.Hais).Select(x => $"[{x.Spec.HaiType}{x.Spec.Number}]")
                .Aggregate("", (a, b) => a + b));
        }

        [ContextMenu("�׽�Ʈ")]
        private void TestYaku()
        {
            var checker =
                (IYakuConditionChecker)Activator.CreateInstance(Type.GetType("MRD." + yakuName + "Checker", true));
            YakuHolderInfo x;
            if (tripleTest == null)
            {
                Debug.Log("no yaku tower");
                return;
            }

            if (completeTest == null)
            {
                Debug.Log("triple tower yaku test");
                x = tripleTest;
            }
            else
            {
                Debug.Log("complete tower yaku test");
                x = completeTest;
            }

            Debug.Log(x.MentsuInfos.SelectMany(x => x.Hais).Select(x => $"[{x.Spec.HaiType}{x.Spec.Number}]")
                .Aggregate("", (a, b) => a + b));
            Debug.Log(checker.CheckCondition(x) ? yakuName + " Yes" : yakuName + " No");
        }
    }
}
