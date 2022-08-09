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
            { typeof(AdditionalExplosionRelic), () => new AdditionalExplosionRelic() },
			{ typeof(AdditionalSupplyRelic), () => new AdditionalSupplyRelic() },
			{ typeof(BlackSmithRelic), () => new BlackSmithRelic() },
			{ typeof(BrandRelic), () => new BrandRelic() },
			{ typeof(BrokenSkullRelic), () => new BrokenSkullRelic() },
			{ typeof(DoraRelic), () => new DoraRelic() },
			{ typeof(EndNEndRelic), () => new EndNEndRelic() },
			{ typeof(FairWindRelic), () => new FairWindRelic() },
			{ typeof(FastExpandRelic), () => new FastExpandRelic() },
			{ typeof(GlueRelic), () => new GlueRelic() },
			{ typeof(HealRelic), () => new HealRelic() },
			{ typeof(HornRelic), () => new HornRelic() },
			{ typeof(JunkShopRelic), () => new JunkShopRelic() },
			{ typeof(LuckySevenRelic), () => new LuckySevenRelic() },
			{ typeof(OneShotRelic), () => new OneShotRelic() },
			{ typeof(PeacePreacherRelic), () => new PeacePreacherRelic() },
			{ typeof(PenetratingWoundRelic), () => new PenetratingWoundRelic() },
			{ typeof(PensionRelic), () => new PensionRelic() },
			{ typeof(RageRelic), () => new RageRelic() },
			{ typeof(SouReinforcementRelic), () => new SouReinforcementRelic() },
			{ typeof(PinReinforcementRelic), () => new PinReinforcementRelic() },
			{ typeof(WanReinforcementRelic), () => new WanReinforcementRelic() },
			{ typeof(RowColorRelic), () => new RowColorRelic() },
			{ typeof(ShockWaveRelic), () => new ShockWaveRelic() },
			{ typeof(SideSupportRelic), () => new SideSupportRelic() },
			{ typeof(StrongWindRelic), () => new StrongWindRelic() },
			{ typeof(SwordNBombRelic), () => new SwordNBombRelic() },
			{ typeof(ThreeColorRelic), () => new ThreeColorRelic() },
			{ typeof(ThroneRelic), () => new ThroneRelic() },
			{ typeof(VibrationDescentRelic), () => new VibrationDescentRelic() },
			{ typeof(YakumanListRelic), () => new YakumanListRelic() },

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
                    AfterAddRelic(index, relic);
                    return true;
                }
            }
            var tmp = RelicInstance[Shop[index]]();
            ownRelics.Add(tmp);
            AfterAddRelic(index, tmp);
            RoundManager.Inst.Grid.UpdateAllTowerStat();
            return true;
        }
        private void AfterAddRelic(int index, Relic obj)
        {
            if (--remainRelics[Shop[index]] == 0) rankRelics[relicsRank[Shop[index]]].Remove(Shop[index]);
            if (relicsRank[Shop[index]] == RelicRank.S) rankRelics[RelicRank.S].Clear();
            obj.Amount++;
            obj.OnBuyAction();
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
