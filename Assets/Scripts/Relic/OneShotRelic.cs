using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class OneShotRelic : Relic
    {
        public override string Name => "OneShot";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.B;
    }
}