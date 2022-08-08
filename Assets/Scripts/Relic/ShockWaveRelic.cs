using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class ShockWaveRelic : Relic
    {
        public override string Name => "ShockWave";
        public override int MaxAmount => 1;
        public override RelicRank Rank => RelicRank.A;

    }
}