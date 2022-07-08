using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        private Vector3 _startLocation;
        private float _speed;

        public int baseDamage;
        
        // Readonly property, should be set with setDirection().
        public Vector3 startLocation{ get; private set; }
        public Vector3 direction{ get; private set; }
        public float speed { get; private set; }

        public void setDirection(Vector3 startLocation, GameObject enemy, float speed){
            this.startLocation = startLocation;
            this.direction = enemy.transform.position - startLocation;
            this.speed = speed;

            this.gameObject.transform.position = startLocation;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = this.direction * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Enemy"){
                collision.gameObject.GetComponent<EnemyController>().health -= baseDamage;
                Destroy(this.gameObject);
            }
        }
    }
}