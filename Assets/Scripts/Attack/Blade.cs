using UnityEngine;

namespace MRD
{
    public class Blade : Attack
    {
        private static Vector3 GetLocation(Transform enemy)
        {
            float r = Random.Range(0.0f, 0.5f);
            var randomPoint = r * Random.onUnitSphere;

            return enemy.position + randomPoint;
        }

        public BladeInfo BladeInfo => (BladeInfo)attackInfo;

        protected override void OnInit()
        {
            var enemy = BladeInfo.Target;
            var enemyT = enemy.transform;

            //enemy로부터 거리가 -0.5 ~ 0.5 지점에 랜덤하게 blade 소환
            var bladeLocation = GetLocation(enemyT);

            if (bladeLocation.z > 0)
                bladeLocation.z = -bladeLocation.z;

            transform.position = bladeLocation;

            var enemyPos = enemyT.position;

            //blade rotate
            float r = Mathf.Atan2(enemyPos.x - bladeLocation.x, enemyPos.y - bladeLocation.y) * Mathf.Rad2Deg;

            if ((enemyPos.x - bladeLocation.x) * (enemyPos.y - bladeLocation.y) >= 0)
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 0 - r);
            }
            else
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 180 - r);
            }

            //blade와 겹치는 enemy 모두에게 damage 적용
        }
    }
}