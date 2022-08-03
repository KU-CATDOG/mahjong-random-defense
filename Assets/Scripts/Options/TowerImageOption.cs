using System;
using System.Collections.Generic;

namespace MRD
{
    public abstract class TowerImageOption : TowerOption
    {
        private static readonly Dictionary<Type, string> towerTypePath = new()
        {
            { typeof(TripleTowerInfo), "TowerSprite/triple_tower" },
        };

        public IReadOnlyList<(int index, int order)> Images =>
            HolderStat.TowerInfo is TripleTowerInfo ? tripleTowerImages : completeTowerImages;

        protected virtual List<(int index, int order)> tripleTowerImages => new();
        protected virtual List<(int index, int order)> completeTowerImages => tripleTowerImages;
    }
}
