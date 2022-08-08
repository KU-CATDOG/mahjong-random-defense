using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class PensionRelic : Relic
    {
        public override string Name => "Pension";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.A;

    }
}