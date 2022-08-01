using UnityEngine;

namespace MRD
{
    internal class Jangpan : Explosive
    {
        float tickTimer = 0f;
        int tickCount = 0;

        protected override void OnUpdate()
        {
            tickTimer += Time.deltaTime * RoundManager.Inst.playSpeed;
            
            if (tickTimer < 0.1f) return;
            tickTimer = 0f;
            OnTick();
            tickCount++;
            
            if (tickCount < 30) return;
            Destroy(gameObject);
        }
        protected override void OnInit()
        {
            transform.position = ExplosiveInfo.Origin;
            transform.localScale = Vector2.one * ExplosiveInfo.Radius;
        }

        private void OnTick()
        {
            var targets = Physics2D.OverlapCircleAll(ExplosiveInfo.Origin, ExplosiveInfo.Radius / 2);
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].tag == "Enemy")
                    if (ExplosiveInfo.Target == null || targets[i].gameObject != ExplosiveInfo.Target.gameObject)
                        targets[i].gameObject.GetComponent<EnemyController>().OnHit(ExplosiveInfo);
            }
            
        }
    }
}