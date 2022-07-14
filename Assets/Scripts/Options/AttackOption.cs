using System;
using System.Collections.Generic;

namespace MRD
{
    public class AttackOption
    {
        public List<Action<EnemyController>> OnhitActions = new();

        public (string imageName, int priority)? projectileImage = null;

        public AttackOption(List<Action<EnemyController>> onhitActions, (string imageName, int priority)? projectileImage)
        {
            OnhitActions = onhitActions;
            this.projectileImage = projectileImage;
        }
    }
}