using System.Collections.Generic;

namespace MRD
{
    public interface IYakuConditionChecker
    {
        public string TargetYakuName { get; }

        public bool CheckCondition(YakuHolderInfo holder);
    }

    public class YakuConditionChecker
    {
        private static YakuConditionChecker instance;

        public static YakuConditionChecker Instance => instance ??= new YakuConditionChecker();

        private readonly List<IYakuConditionChecker> normalYakuChechers = new();
        private readonly List<IYakuConditionChecker> yakumanChechers = new();

        private YakuConditionChecker()
        {
            normalYakuChechers.Add(new TanYaoChecker());
        }
    }
}
