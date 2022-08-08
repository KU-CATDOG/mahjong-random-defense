using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class JunkShopRelic : Relic
    {
        public override string Name => "JunkShop";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;

    }
}