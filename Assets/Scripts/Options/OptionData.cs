using System;
using System.Collections.Generic;

namespace MRD
{
    public static class OptionData
    {
        private static readonly Dictionary<string, Func<TowerOption>> towerOptionBuilders =
            new Dictionary<string, Func<TowerOption>>
            {
                { nameof(DoraStatOption), () => new DoraStatOption() },
                { nameof(ChanTaStatOption), () => new ChanTaStatOption() },
            };

        public static TowerOption GetOption(string name)
        {
            return towerOptionBuilders.TryGetValue(name, out var v) ? v() : null;
        }
    }
}
