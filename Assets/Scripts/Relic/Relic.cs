using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public abstract class Relic
    {
        public virtual string Name => null;
        public virtual RelicRank Rank => RelicRank.C;
        public virtual int MaxAmount => 0;
        public virtual Stat AdditionalStat(TowerStat towerStat) => new();

        public virtual void OnBuyAction() { }

        public int Amount = 0;
    }
    public enum RelicRank { C, B, A, S }
}
