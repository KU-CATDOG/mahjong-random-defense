using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public class HealRelic : Relic
    {
        public override string Name => "Heal";
        public override int MaxAmount => 3;
        public override RelicRank Rank => RelicRank.C;
        public override void OnBuyAction() => RoundManager.Inst.PlayerHeal(5000);
        
    }
}