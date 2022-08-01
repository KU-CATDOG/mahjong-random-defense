using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class EnemyStatusEffect
    {
        public static readonly (float duration, int maxStack)[,] statusInfo =
        {
            { (.1f, 1), (.5f, 1), (1f, 1), (2f, 2) },
            { (.5f, 1), (1f, 2), (2f, 4), (5f, 5) },
        };

        private readonly EnemyStatusEffectType type;

        public EnemyStatusEffect(EnemyStatusEffectType statusType) => type = statusType;

        public float remainTime { get; private set; }
        public int stackCount { get; private set; }

        public void UpdateTime()
        {
            if (remainTime <= 0) return;
            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                remainTime = 0;
                stackCount = 0;
            }
        }

        public void GainStatusEffect(int statusEffectLevel)
        {
            var info = statusInfo[(int)type, statusEffectLevel - 1];
            if (info.duration > remainTime) remainTime = info.duration;
            if (stackCount < info.maxStack) stackCount++;
        }
    }

    public class EnemyStatusEffectList
    {
        private const int statusEffectCount = 2;
        private readonly List<EnemyStatusEffect> effects = new();

        public EnemyStatusEffectList()
        {
            for (int i = 0; i < statusEffectCount; i++) effects.Add(new EnemyStatusEffect((EnemyStatusEffectType)i));
        }

        public int this[EnemyStatusEffectType type] => effects[(int)type].stackCount;

        public void UpdateListTime()
        {
            for (int i = 0; i < statusEffectCount; i++) effects[i].UpdateTime();
        }

        public void GainStatusEffect(EnemyStatusEffectType type, int statusEffectLevel)
        {
            effects[(int)type].GainStatusEffect(statusEffectLevel);
        }
    }

    public enum EnemyStatusEffectType
    {
        WanLoot = 0,
        PinSlow = 1,
    }
}
