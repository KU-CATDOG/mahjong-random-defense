using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public abstract class Relic
    {
        public virtual Stat AdditionalStat => new();

    }
}
