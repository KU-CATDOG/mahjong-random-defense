using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class PenetratingWoundRelic : Relic
    {
        public override string Name => "PenetratingWound";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;
    }
}