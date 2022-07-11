using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        private TowerStat TowerStat;
        
        // Readonly property, should be set with setDirection().
        public Vector3 StartLocation{ get; private set; }
        public Vector3 Direction{ get; private set; }
        public float Speed { get; private set; }

        public void InitBullet(Vector3 startLocation, GameObject enemy, TowerStat towerStat){
            StartLocation = startLocation;
            TowerStat = towerStat;
            //var targetTime = (enemy.transform.position - startLocation).magnitude / (enemy.GetComponent<Rigidbody2D>().velocity);
            //var targetVector = 0;

            Direction = (enemy.transform.position - startLocation).normalized;
            Speed = towerStat.BaseAttackSpeed;

            this.gameObject.transform.position = startLocation;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Enemy"){
                collision.gameObject.GetComponent<EnemyController>().OnHit(TowerStat);
                Destroy(this.gameObject);
            }
        }
    }
}