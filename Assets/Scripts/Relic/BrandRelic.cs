using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class BrandRelic : Relic
    {
        public override string Name => "Brand";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;

    }
}