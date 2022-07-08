using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Bullet : Attack
    {
        private Vector3 _startLocation;
        private float _speed;
        public Vector3 startLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                this.transform.position = _startLocation;
            }
        }
        public int baseDamage;
        
        // Readonly property, should be set with setDirection().
        public Vector3 direction{ get; private set; }
        public float speed { get; private set; }

        public void setDirection(Vector3 direction, float speed){
            this.direction = direction;
            this.speed = speed;

            this.gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
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