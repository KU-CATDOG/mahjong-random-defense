using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public abstract class AttackBehaviour
    {
        public Tower Tower { get; private set; }

        public void Init(Tower tower)
        {
            Tower = tower;
        }

        public abstract void OnUpdate();
    }
}
