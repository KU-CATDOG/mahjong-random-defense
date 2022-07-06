using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class EnemyStatusEffect
    {
        public float remainTime { get; private set; }
        public int statusLevel { get; private set; }
        public void UpdateTime()
        {
            if (remainTime <= 0) return;
            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                remainTime = 0;
                statusLevel = 0;
            }
        }

        public void GainStatusEffect(float duration, int maxLevel)
        {
            if (duration > remainTime) remainTime = duration;
            if (statusLevel < maxLevel) statusLevel++;
        }
    }

    public class EnemyStatusEffectList
    {
        private const int statusEffectCount = 2;
        private readonly List<EnemyStatusEffect> effects = new();
        public int this[EnemyStatusEffectType type] => effects[(int)type].statusLevel;

        public EnemyStatusEffectList()
        {
            for (int i = 0; i < statusEffectCount; i++)
            {
                effects.Add(new());
            }
        }
        public void UpdateListTime()
        {
            for (int i = 0; i < statusEffectCount; i++)
            {
                effects[i].UpdateTime();
            }
        }
    }

    public enum EnemyStatusEffectType
    {
        WanLoot = 0,
        PinSlow = 1,
    }
}