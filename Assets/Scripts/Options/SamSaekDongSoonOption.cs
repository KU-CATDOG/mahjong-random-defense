using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class SamSaekDongSoonStatOption : TowerStatOption
    {
        public override string Name => nameof(SamSaekDongSoonStatOption);

        public override float AdditionalAttackSpeedMultiplier => additionalAttackSpeedMultiplier;
        public override float AdditionalAttack => additionalAttack;

        private float additionalAttackSpeedMultiplier;
        private float additionalAttack;
        protected override void OnAttachOption()
        {
            var info = (YakuHolderInfo)HolderInfo;
            var isComplete = info is CompleteTowerInfo;

            (additionalAttack, additionalAttackSpeedMultiplier) = (isComplete, info.isMenzen) switch
            {
                (false, false) => (0f, 1.2f),
                (false, true) => (15f, 1.2f),
                (true, false) => (0f, 1.3f),
                (true, true) => (50f, 1.3f),
            };
        }
    }
    public class SamSaekDongSoonEtcOption : TowerEtcOption
    {
        public override string Name => nameof(SamSaekDongSoonEtcOption);

        public override IReadOnlyList<Func<AttackOption>> OnShootOption => new List<Func<AttackOption>>()
        {
            () => UnityEngine.Random.Range(0, 3) switch
            {
                0 => new AttackOption(null, null), //wan
                1 => new AttackOption(null, null), //pin
                2 => new AttackOption(null, null), //Sou
                _ => null
            }
        };
    }
}
