using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class GlueRelic : Relic
    {
        public override string Name => "Glue";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;

    }
}