using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        public Yaku(string name, string[] optionNames, bool isYakuman)
        {
            Name = name;
            OptionNames = optionNames;
            IsYakuman = isYakuman;
        }

        public string Name { get; }

        public string[] OptionNames { get; }

        public bool IsYakuman { get; }
    }

    public class YakuConditionChecker
    {
        private static YakuConditionChecker instance;

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
            new YiPeKoChecker(),
            new RyangPeKoChecker(),
        };

        private readonly Dictionary<string, string[]> upperYakuList = new()
        {
            { "ChanTa", new[] { "HonNoDu", "JunJJang" } },
            { "HonIlSaek", new[] { "CheongIlSaek" } },
            { "YiPeKo", new[] { "RyangPeKo" } },
        };

        private readonly List<IYakuConditionChecker> yakumanCheckers = new()
        {
            new DaeSamWonChecker(),
            new SoSaHeeChecker(),
            new DaeSaHeeChecker(),
            new DaeChilSeongChecker(),
            new ShuAnKouChecker(),
            new ShuKantSuChecker(),
            new JaIlSaekChecker(),
            new CheongNoDuChecker(),
            new NokIlSaekChecker(),
            new GuRyeonBoDeungChecker(),
            new GukSaMuSangChecker()
        };

        public static YakuConditionChecker Instance => instance ??= new YakuConditionChecker();

        public List<Yaku> GetYakuList(YakuHolderInfo holder)
        {
            if (holder is not TripleTowerInfo)
            {
                var yakumans = yakumanCheckers.Where(x => x.CheckCondition(holder)).ToList();

                if (yakumans.Count > 0)
                    return yakumans.Select(x => new Yaku(x.TargetYakuName, x.OptionNames, true)).ToList();
            }

            var normalYakus = normalYakuCheckers.Where(x => x.CheckCondition(holder))
                .Select(x => new Yaku(x.TargetYakuName, x.OptionNames, false));

            return normalYakus.Where(x => !upperYakuList.TryGetValue(x.Name, out string[] uppers) || uppers.All(y => normalYakus.All(z => z.Name != y))).ToList();

        }
    }
}
