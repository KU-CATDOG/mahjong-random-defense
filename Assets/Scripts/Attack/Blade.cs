using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Blade : Attack
    {
        private TowerStat TowerStat;

        public Vector3 BladeLocation { get; private set; }

        private Vector3 GetLocation(GameObject enemy)
        {
            Vector3 RandomPoint = Random.onUnitSphere;
            RandomPoint.y = 0.0f;

            float r = Random.Range(0.0f, 0.5f);
            RandomPoint = r * RandomPoint.normalized;

            return enemy.transform.position + RandomPoint;
        }

        public void SetBlade(GameObject enemy, TowerStat towerStat)
        {
            TowerStat = towerStat;

            //enemy로부터 거리가 -0.5 ~ 0.5 지점에 랜덤하게 blade 소환
            BladeLocation = GetLocation(enemy);

            //enemy와 blade의 기울기만큼 blade rotate
            transform.rotation = Quaternion.Euler(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().OnHit(TowerStat);
                Destroy(this.gameObject);
            }
        }
    }
}