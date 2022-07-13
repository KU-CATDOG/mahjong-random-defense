using System;
using System.Collections.Generic;
using System.Linq;

namespace MRD
{
    public class ToiToiStatOption : TowerStatOption
    {
        public override string Name => nameof(ToiToiStatOption);

        public override int AdditionalAttackPercent => HolderInfo is CompleteTowerInfo ? 20 : 5;
    }
    public class ToiToiOption : TowerEtcOption
    {
        public override string Name => nameof(SamSaekDongSoonEtcOption);

        public override IReadOnlyList<BulletInfo> AdditionalBullet => new List<BulletInfo>() { new(1f, 30f), new(1f, -30f)};
    }
}
