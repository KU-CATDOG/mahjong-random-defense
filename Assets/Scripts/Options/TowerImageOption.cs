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

        public virtual List<(int index, int order)> Images => new();  
    }
}
