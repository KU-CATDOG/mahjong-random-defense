using System;
using System.Collections.Generic;

namespace MRD
{
    public abstract class TowerEtcOption : TowerOption
    {
        public virtual IReadOnlyList<Action<EnemyController>> OnhitActions => new List<Action<EnemyController>>();

        public virtual (string imageName, int priority)? projectileImage => null;

        public virtual IReadOnlyList<BulletInfo> AdditionalBullet => new List<BulletInfo>();

        public virtual IReadOnlyList<Func<AttackOption>> OnShootOption => new List<Func<AttackOption>>();
    }
}
