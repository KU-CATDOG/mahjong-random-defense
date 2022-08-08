using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class YakumanListRelic : Relic
    {
        public override string Name => "YakumanList";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;

    }
}