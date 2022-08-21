using UnityEngine;
using System;
namespace MRD
{
    public class Blade : Attack
    {
        public BladeInfo BladeInfo => (BladeInfo)attackInfo;
        private Vector3 origin;
        private Vector3 direction;
        private float timer = 0f;
        private bool en = false;
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            transform.localScale = new Vector2(0.3f, 4.0f * EaseOutCubic((timer>0.7f?0.7f:timer)/0.7f));
            transform.position = origin - direction * 2.0f * EaseOutCubic((timer>0.7f?0.7f:timer)/0.7f);

            if (timer > 1f)
                Destroy(gameObject);
        }
        private static Vector3 GetLocation(Transform enemy)
        {
            float r = UnityEngine.Random.Range(0.0f, 0.5f);
            var tmpPoint = UnityEngine.Random.onUnitSphere;
            tmpPoint.z = 0;
            var randomPoint = r * tmpPoint.normalized;

            return enemy.position + randomPoint;
        }

        protected override void OnInit()
        {
            var enemy = BladeInfo.Target;
            if (enemy == null)
            {
                Destroy(gameObject);
                return;
            }

            var enemyT = enemy.transform;

            //enemy로부터 거리가 -0.5 ~ 0.5 지점에 랜덤하게 blade 소환
            var bladeLocation = GetLocation(enemyT);

            if (bladeLocation.z > 0)
                bladeLocation.z = -bladeLocation.z;

            transform.position = bladeLocation;

            var enemyPos = enemyT.position;

            //blade rotate
            float r = UnityEngine.Random.Range(-180f, 180f);

            transform.Rotate(0.0f, 0.0f, r);
            direction = MathHelper.RotateVector(Vector3.up, r);
            transform.position += direction * 1.5f;
            origin = transform.position;


            //blade와 겹치는 enemy 모두에게 damage 적용
            var targets = Physics2D.OverlapBoxAll(bladeLocation, new Vector2(0.3f, 4.0f), r);

            for (int i = 0; i < targets.Length; i++)
            {
                // Debug.Log(targets[i].name);
                try {
                    if (BladeInfo.damageToTarget ||
                        targets[i].tag == "Enemy" && targets[i].gameObject != BladeInfo.Target.gameObject)
                        {
                            targets[i].gameObject.TryGetComponent(out EnemyController enemyController);
                            if(enemyController is not null)
                                enemyController?.OnHit(BladeInfo);
                        }
                } catch (NullReferenceException _) {
                    Debug.Log("Blade Target NULL!: " + _.ToString());
                }
            }
            en = true;

            //Destroy(gameObject, 1);
        }
        private float EaseOutCubic(float t) // 0~1
            =>
                1 - Mathf.Pow(1 - t, 3);
    }
}
