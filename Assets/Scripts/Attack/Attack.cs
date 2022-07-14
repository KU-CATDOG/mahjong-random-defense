using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MRD
{
    public abstract class Attack : MonoBehaviour
    {
        protected AttackInfo attackInfo;

        public void Init(AttackInfo info)
        {
            attackInfo = info;
        }

        // 여기서 스프라이트를 세팅하던 할것
        protected abstract void OnInit();
    }

    public static class AttackGenerator
    {
        private static readonly Dictionary<AttackType, string> attackPrefabMap = new()
        {
            { AttackType.Bullet, Path.Combine("Prefabs", "Bullet.prefab") },
        };

        public static T GenerateAttack<T>(AttackInfo info) where T : Attack
        {
            var attackPrefab = ResourceDictionary.Get<T>(attackPrefabMap[info.AttackType]);

            var attack = Object.Instantiate(attackPrefab, info.StartPosition, Quaternion.identity);

            attack.Init(info);

            return attack;
        }
    }
}