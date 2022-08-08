using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class AdditionalSupplyRelic : Relic
    {
        public override string Name => "AdditionalSupply";
        public override int MaxAmount => 2;
        public override RelicRank Rank => RelicRank.A;

    }
}