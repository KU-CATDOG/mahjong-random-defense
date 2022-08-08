using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class RageRelic : Relic
    {
        public override string Name => "Rage";
        public override int MaxAmount => 5;
        public override RelicRank Rank => RelicRank.B;
        
    }
}