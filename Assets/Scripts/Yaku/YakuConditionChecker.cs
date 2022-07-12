using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public interface IYakuConditionChecker
    {
        public string TargetYakuName { get; }

        public string[] OptionNames { get; }

        public bool CheckCondition(YakuHolderInfo holder);
    }

    public class Yaku
    {
        public string Name { get; }

        public string[] OptionNames { get; }

        public bool IsYakuman { get; }

        public Yaku(string name, string[] optionNames, bool isYakuman)
        {
            Name = name;
            OptionNames = optionNames;
            IsYakuman = isYakuman;
        }
    }

    public class YakuConditionChecker
    {
        private static YakuConditionChecker instance;

        public static YakuConditionChecker Instance => instance ??= new YakuConditionChecker();

        private readonly Dictionary<string, string[]> upperYakuList = new()
        {
            {"ChanTa", new[]{"HonNoDu", "JunJJang"}},
        };

        private readonly List<IYakuConditionChecker> normalYakuChechers = new()
        {
            new TanYaoChecker(),
        };

        private readonly List<IYakuConditionChecker> yakumanChechers = new()
        {
            new DaeSaHeeChecker(),
        };

        public List<Yaku> GetYakuList(YakuHolderInfo holder)
        {
            var yakumans = yakumanChechers.Where(x => x.CheckCondition(holder)).ToList();

            if (yakumans.Count > 0)
            {
                return yakumans.Select(x => new Yaku(x.TargetYakuName, x.OptionNames, true)).ToList();
            }

            var normalYakus = normalYakuChechers.Select(x => new Yaku(x.TargetYakuName, x.OptionNames, false)).ToList();
            var normalYakuNames = new HashSet<string>(normalYakus.Select(x => x.Name));
            for (var i = normalYakus.Count - 1; i > 0; i--)
            {
                var yaku = normalYakus[i];

                if (!upperYakuList.TryGetValue(yaku.Name, out var uppers)) continue;

                foreach (var upper in uppers)
                {
                    if (!normalYakuNames.Contains(upper)) continue;

                    normalYakus.RemoveAt(i);
                }
            }

            return normalYakus;
        }
    }
}
