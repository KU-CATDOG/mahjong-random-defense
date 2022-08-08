using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class RelicManager
    {
        public static readonly Dictionary<RelicRank, int> RelicCost = new()
        { { RelicRank.C, 5 }, { RelicRank.B, 8 }, { RelicRank.A, 15 }, { RelicRank.S, 20 }};

        public static readonly Dictionary<Type, Func<Relic>> RelicInstance = new()
        {
            { typeof(SouReinforcementRelic), () => new SouReinforcementRelic() },
            { typeof(PinReinforcementRelic), () => new PinReinforcementRelic() },
            { typeof(WanReinforcementRelic), () => new WanReinforcementRelic() },
        };

        private Dictionary<RelicRank, List<Type>> rankRelics = new()
        {
            { RelicRank.C, new List<Type>() },
            { RelicRank.B, new List<Type>() },
            { RelicRank.A, new List<Type>() },
            { RelicRank.S, new List<Type>() }
        };
        private Dictionary<Type, RelicRank> relicsRank = new();
        private Dictionary<Type, int> remainRelics = new();
        private List<Relic> ownRelics = new();
        public IReadOnlyList<Relic> OwnRelics => ownRelics;

        public Type[] Shop = new Type[3];

        public int RefreshCost { get; private set; }

        public int[] RankProb = new int[4] { 2, 13, 40, 45 };

        public RelicManager()
        {
            foreach (var type in RelicInstance.Keys)
            {
                Relic tmp = RelicInstance[type]();
                rankRelics[tmp.Rank].Add(type);
                remainRelics.Add(type, tmp.MaxAmount);
                relicsRank.Add(type, tmp.Rank);
            }
        }

        public bool BuyRelic(int index)
        {
            if (Shop[index] == null || !RoundManager.Inst.MinusTsumoToken(RelicCost[relicsRank[Shop[index]]])) return false;

            foreach (var relic in ownRelics)
            {
                if (relic.GetType() == Shop[index])
                {
                    relic.Amount++;
                    AfterAddRelic(index);
                    return true;
                }
            }
            ownRelics.Add(RelicInstance[Shop[index]]());
            AfterAddRelic(index);
            return true;
        }
        private void AfterAddRelic(int index)
        {
            if (--remainRelics[Shop[index]] == 0) rankRelics[relicsRank[Shop[index]]].Remove(Shop[index]);
            if (relicsRank[Shop[index]] == RelicRank.S) rankRelics[RelicRank.S].Clear();
            ownRelics.Find(x => x.GetType() == Shop[index]).OnBuyAction();
            Shop[index] = null;
        }

        public int RelicNum(Type relicType)
        {
            foreach (var relic in ownRelics)
            {
                if (relic.GetType() == relicType)
                {
                    return relic.Amount;
                }
            }
            return 0;
        }
        public int this[Type relicType] => RelicNum(relicType);

        public void ResetRefreshCost() => RefreshCost = 0;

        public bool Refresh(bool isFree = false)
        {
            if (!isFree && !RoundManager.Inst.MinusTsumoToken(RefreshCost)) return false;
            for (int i = 0; i < 3; i++) Shop[i] = null;
            var newProb = RankProb.Clone() as int[];
            var newList = new List<Type>[4];
            for (int i = 0; i < 4; i++) newList[i] = new(rankRelics[(RelicRank)i]);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (newList[j].Count == 0) newProb[j] = 0;
                }
                if (newProb.Sum() == 0) break;
                int rand = UnityEngine.Random.Range(0, newProb.Sum());
                for (int j = 0; j < 4; j++)
                {
                    rand -= newProb[j];
                    if (rand < 0)
                    {
                        var idx = UnityEngine.Random.Range(0, newList[j].Count);
                        Shop[i] = newList[j][idx];
                        newList[j].RemoveAt(idx);
                        break;
                    }
                }
            }
            if (!isFree) RefreshCost += 2;
            return true;
        }
    }
}
