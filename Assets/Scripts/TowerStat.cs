using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class TowerStat
    {
        public Tower Holder { get; }

        public TowerStat(Tower t)
        {
            Holder = t;
        }

        // 아무것도 안해도 모든 TowerStat이 기본적으로 가지는 옵션들
        private static readonly IReadOnlyList<string> defaultOptionNames = new[]
        {
            nameof(DoraStatOption),
        };

        private readonly List<TowerOption> options = new();

        public int BaseAttack => Holder.TowerInfo.Hais.Count * 10;

        public float BaseAttackSpeed => 1;

        public float FinalAttackMultiplier { get; private set; } = 1;

        public float BaseCritChance => 0;

        public float BaseCritMultiplier = 2;

        public float AdditionalAttack { get; private set; }

        public int AdditionalAttackPercent { get; private set; }

        public float AdditionalAttackSpeedMultiplier { get; private set; } = 1;

        public float AdditionalCritChance { get; private set; }


        public float AdditionalCritMultiplier { get; private set; }

        public float FinalAttack => (BaseAttack + AdditionalAttack) * (1 + AdditionalAttackPercent / 100f) * FinalAttackMultiplier;

        public float FinalAttackSpeed => BaseAttackSpeed * AdditionalAttackSpeedMultiplier;

        public float FinalCritChance => BaseCritChance + AdditionalCritChance;
        public float FinalCritMultiplier => BaseCritMultiplier + AdditionalCritMultiplier;

        public void UpdateOptions()
        {
            if (Holder == null) return;

            var newOptions = new HashSet<string>();

            foreach (var i in defaultOptionNames)
            {
                newOptions.Add(i);
            }

            if (Holder.TowerInfo is YakuHolderInfo h)
            {
                h.UpdateYaku();

                foreach (var i in h.YakuList.SelectMany(x => x.OptionNames))
                {
                    newOptions.Add(i);
                }
            }

            // 원래 있던 옵션이 없어졌으면 Dispose 하고 제거함
            for (var i = options.Count - 1; i > 0; i--)
            {
                var oldOption = options[i];
                if (newOptions.Contains(oldOption.Name)) continue;

                oldOption.Dispose();
                options.RemoveAt(i);
            }

            // 원래 없었는데 새로 생긴 옵션이 있으면 Attach 해줌
            foreach (var newOption in from i in newOptions where options.All(x => x.Name != i) select OptionData.GetOption(i))
            {
                newOption.AttachOption(this);
                options.Add(newOption);
            }
        }

        public void UpdateStat()
        {
            AdditionalAttack = 0;
            AdditionalAttackPercent = 0;
            AdditionalCritChance = 0;
            AdditionalCritMultiplier = 1;
            AdditionalAttackSpeedMultiplier = 1;

            foreach (var o in options)
            {
                if (o is not TowerStatOption so) continue;

                AdditionalAttack += so.AdditionalAttack;
                AdditionalAttackPercent += so.AdditionalAttackPercent;
                AdditionalCritChance += so.AdditionalCritChance;
                AdditionalCritMultiplier += so.AdditionalCritMultiplier;
                AdditionalAttackSpeedMultiplier *= so.AdditionalAttackSpeedMultiplier;
            }
        }
    }
}
