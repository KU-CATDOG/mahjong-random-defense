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
            OnInit();
        }

        // 여기서 스프라이트를 세팅하던 할것
        protected abstract void OnInit();
    }

    public static class AttackGenerator
    {
        // TODO: Cannon, Missile 이미지
        private static readonly Dictionary<AttackImage, string> attackPrefabMap = new()
        {
            { AttackImage.Default, Path.Combine("Prefabs", "Bullet") },
            { AttackImage.Sou, Path.Combine("Prefabs", "Bullets","SouBullet") },
            { AttackImage.Pin, Path.Combine("Prefabs", "Bullets","PinBullet") },
            { AttackImage.Wan, Path.Combine("Prefabs", "Bullets","WanBullet") },
            { AttackImage.SSDG, Path.Combine("Prefabs", "Bullets","SSDGBullet") },
            { AttackImage.Cannon, Path.Combine("Prefabs", "Bullets","CannonBullet") },
            { AttackImage.Missile, Path.Combine("Prefabs", "Bullets","WanBullet") },
            { AttackImage.Grenade, Path.Combine("Prefabs", "Bullets","Grenade") },
            { AttackImage.Blade, Path.Combine("Prefabs", "Blade") },
        };

        public static T GenerateAttack<T>(AttackInfo info) where T : Attack
        {
            var attackPrefab = ResourceDictionary.Get<GameObject>(attackPrefabMap[info.ImageName]).GetComponent<T>();

            var rotation = info is not BulletInfo bulletInfo? Quaternion.identity : Quaternion.Euler(new(0,0,GetAngle(Vector3.up, bulletInfo.Direction)*2));
            var attack = Object.Instantiate(attackPrefab, info.StartPosition, rotation );

            attack.Init(info);

            return attack;
        }
        private static float GetAngle (Vector3 vStart, Vector3 vEnd)
        {
            Vector3 v = vEnd - vStart;
    
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }
 
    }
}