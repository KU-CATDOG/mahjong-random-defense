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
            float r = Random.Range(0.0f, 0.5f);
            Vector3 RandomPoint = r * Random.onUnitSphere;

            return enemy.transform.position + RandomPoint;
        }
        public void SetBlade(GameObject enemy, TowerStat towerStat)
        {
            TowerStat = towerStat;

            //enemy로부터 거리가 -0.5 ~ 0.5 지점에 랜덤하게 blade 소환
            BladeLocation = GetLocation(enemy);
            this.gameObject.transform.position = BladeLocation;

            if(this.gameObject.transform.position.z > 0)
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0 - transform.position.z);

            //blade rotate
            float r = Mathf.Atan2(enemy.transform.position.x - this.gameObject.transform.position.x, enemy.transform.position.y - this.gameObject.transform.position.y) * Mathf.Rad2Deg;
            if ((enemy.transform.position.x - this.gameObject.transform.position.x) * (enemy.transform.position.y - this.gameObject.transform.position.y) >= 0) this.gameObject.transform.Rotate(0.0f, 0.0f, 0-r);
            else this.gameObject.transform.Rotate(0.0f, 0.0f, 180-r);
        }

        //blade와 겹치는 enemy 모두에게 damage 적용
    }
}