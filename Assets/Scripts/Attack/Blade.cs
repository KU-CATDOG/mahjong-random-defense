using UnityEngine;

namespace MRD
{
    public class Blade : Attack
    {
        public BladeInfo BladeInfo => (BladeInfo)attackInfo;
        float timer = 0f;
        private void Update()
        {
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if (timer > 1f)
                Destroy(gameObject);
        }
        private static Vector3 GetLocation(Transform enemy)
        {
            float r = Random.Range(0.0f, 0.5f);
            var tmpPoint = Random.onUnitSphere;
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

            gameObject.transform.position = bladeLocation;

            var enemyPos = enemyT.position;

            //blade rotate
            float r = UnityEngine.Random.Range(-180f, 180f);

            gameObject.transform.Rotate(0.0f, 0.0f, r);
            gameObject.transform.position += MathHelper.RotateVector(Vector3.up, r)*1.5f;


            //blade와 겹치는 enemy 모두에게 damage 적용
            var targets = Physics2D.OverlapBoxAll(bladeLocation, new Vector2(0.3f, 4.0f), r);

            for (int i = 0; i < targets.Length; i++)
            {
                // Debug.Log(targets[i].name);

                if (BladeInfo.damageToTarget ||
                    targets[i].tag == "Enemy" && targets[i].gameObject != BladeInfo.Target.gameObject)
                    targets[i].gameObject.GetComponent<EnemyController>().OnHit(BladeInfo);
            }

            //Destroy(gameObject, 1);
        }
    }
}
