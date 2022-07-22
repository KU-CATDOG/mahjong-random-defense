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
            {"HonIlSaek", new[]{ "CheongIlSaek" } }
        };

        private readonly List<IYakuConditionChecker> normalYakuCheckers = new()
        {
            new TanYaoChecker(),
            new PingHuChecker(),
            new SanKantSuChecker(),
            new ToiToiChecker(),
            new ChiToiChecker(),
            new ChanTaChecker(),
            new JunJJangChecker(),
            new SamSaekDongGakChecker(),
            new SamSaekDongSoonChecker(),
            new SamWonPaeYeokPaeBaekChecker(),
            new SamWonPaeYeokPaeBalChecker(),
            new SamWonPaeYeokPaeJoongChecker(),
            new JangPungPaeYeokPaeChecker(),
            new SoSamWonChecker(),
            new HonIlSaekChecker(),
            new CheongIlSaekChecker(),
            new HonNoDuChecker(),
            new IlGiTongGwanChecker(),
            new SanAnKeoChecker(),
            new YiPeKoChecker()
        };

        private readonly List<IYakuConditionChecker> yakumanCheckers = new()
        {
            new DaeSaHeeChecker(),
        };

        public List<Yaku> GetYakuList(YakuHolderInfo holder)
        {
            var yakumans = yakumanCheckers.Where(x => x.CheckCondition(holder)).ToList();

            if (yakumans.Count > 0)
            {
                return yakumans.Select(x => new Yaku(x.TargetYakuName, x.OptionNames, true)).ToList();
            }

            var normalYakus = normalYakuCheckers.Where(x => x.CheckCondition(holder)).Select(x => new Yaku(x.TargetYakuName, x.OptionNames, false)).ToList();
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
