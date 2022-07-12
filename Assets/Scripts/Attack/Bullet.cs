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

        private bool IsActivated = false;
        private void FixedUpdate() => Move();

        private void Move()
        {
            if(!IsActivated) return;
            transform.position += Direction * Speed;
        }

        public void InitBullet(Vector3 startLocation, GameObject enemy, float bulletSpeed, TowerStat towerStat){
            StartLocation = startLocation;
            TowerStat = towerStat;

            Direction = (enemy.transform.position - startLocation).normalized;
            Speed = bulletSpeed;

            this.gameObject.transform.position = startLocation;
            IsActivated = true;
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