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

        private Vector3 ExpectedLocation(Vector3 bP, float bV, Vector3 eP, Vector3 eV)
        {
            // (Vxˆ2 +Vyˆ2 - sˆ2) * tˆ2 + 2* (Vx*(Ax - Tx) + Vy*(Ay - Ty)) *t +(Ay - Ty)ˆ2 + (Ax - Tx)ˆ2 = 0;
            var t = QuadraticEquation((eV.x * eV.x + eV.y * eV.y - bV * bV),
                                      2 * (eV.x * (eP.x - bP.x) + eV.y * (eP.y - bP.y)),
                                      (eP.y - bP.y) * (eP.y - bP.y) + (eP.x - bP.x) * (eP.x - bP.x));
            return eV * t;
        }
        private float QuadraticEquation(float a, float b, float c)
        {
            return (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        }
    }
}