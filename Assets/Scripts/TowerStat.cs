using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class TowerStat
    {
        public TowerStat(TowerInfo t)
        {
            TowerInfo = t;
        }

        public TowerInfo TowerInfo { get; }

        // 아무것도 안해도 모든 TowerStat이 기본적으로 가지는 옵션들
        private static readonly IReadOnlyList<string> defaultOptionNames = new[]
        {
            nameof(DoraStatOption),
        };

        private readonly Dictionary<string, TowerOption> options = new();

        public IReadOnlyDictionary<string, TowerOption> Options => options;

        public int BaseAttack => TowerInfo.Hais.Count * 10;

        public float BaseAttackSpeed => 1;

        public float AdditionalAttackMultiplier { get; private set; } = 1;

        public float BaseCritChance => 0;

        public float BaseCritMultiplier = 2;

        public float AdditionalAttack { get; private set; }

        public float AdditionalAttackPercent { get; private set; }

        public float AdditionalAttackSpeedMultiplier { get; private set; } = 1;

        public float AdditionalCritChance { get; private set; }


        public float AdditionalCritMultiplier { get; private set; }

        public float FinalAttack => (BaseAttack + AdditionalAttack) * (1 + AdditionalAttackPercent / 100f) * AdditionalAttackMultiplier;

        public float FinalAttackSpeed => BaseAttackSpeed * AdditionalAttackSpeedMultiplier;

        public float FinalCritChance => BaseCritChance + AdditionalCritChance;
        public float FinalCritMultiplier => BaseCritMultiplier + AdditionalCritMultiplier;

        public (string imageName, int priority) projectileImage = ("normal", 0);

        private readonly List<TowerProcessAttackInfoOption> onAttackOptions = new();

        public void UpdateOptions()
        {
            if(TowerInfo == null) return;
            var newOptions = new HashSet<string>();

            foreach (var i in defaultOptionNames)
            {
                newOptions.Add(i);
            }

            foreach (var i in TowerInfo.DefaultOptions)
            {
                newOptions.Add(i);
            }

            if (TowerInfo is YakuHolderInfo h)
            {
                h.UpdateYaku();

                foreach (var i in h.YakuList.SelectMany(x => x.OptionNames))
                {
                    newOptions.Add(i);
                }
            }

            var toRemove = new List<string>();

            // 원래 있던 옵션이 없어졌으면 Dispose 하고 제거함
            foreach (var oldOption in options.Values)
            {
                if (newOptions.Contains(oldOption.Name)) continue;

                oldOption.Dispose();
                toRemove.Add(oldOption.Name);
            }

            foreach (var r in toRemove)
            {
                options.Remove(r);
            }

            // 원래 없었는데 새로 생긴 옵션이 있으면 Attach 해줌
            foreach (var newOption in from i in newOptions where options.Values.All(x => x.Name != i) select OptionData.GetOption(i))
            {
                if(newOption == null) continue;
                if (options.ContainsKey(newOption.Name)) continue;
                newOption.AttachOption(this);

                options[newOption.Name] = newOption;
            }
            UpdateStat();
        }

        public void UpdateStat()
        {
            AdditionalAttack = 0;
            AdditionalAttackPercent = 0;
            AdditionalCritChance = 0;
            AdditionalCritMultiplier = 1;
            AdditionalAttackSpeedMultiplier = 1;
            AdditionalAttackMultiplier = 1;

            foreach (var o in options.Values)
            {
                switch (o)
                {
                    case TowerStatOption so:
                        AdditionalAttack += so.AdditionalAttack;
                        AdditionalAttackPercent += so.AdditionalAttackPercent;
                        AdditionalCritChance += so.AdditionalCritChance;
                        AdditionalCritMultiplier += so.AdditionalCritMultiplier;
                        AdditionalAttackSpeedMultiplier *= so.AdditionalAttackSpeedMultiplier;
                        AdditionalAttackMultiplier *= so.AdditionalAttackMultiplier;
                        break;
                    case TowerProcessAttackInfoOption oao:
                        onAttackOptions.Add(oao);
                        break;
                }
            }

            onAttackOptions.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }

        public List<AttackInfo> ProcessAttackInfo(AttackInfo info)
        {
            var result = new List<AttackInfo> { info };

            foreach (var o in onAttackOptions)
            {
                o.ProcessAttackInfo(result);
            }

            return result;
        }
    }
}
