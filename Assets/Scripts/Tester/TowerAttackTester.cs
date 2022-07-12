using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD {
    public class TowerAttackTester : MonoBehaviour
    {
        public GameObject tower;
        public GameObject enemy;
        public GameObject bullet;
        // Start is called before the first frame update
        void Start()
        {
            tower.GetComponent<Tower>().TempInit();
            //StartCoroutine("shootBullet",1);
        }

        IEnumerator shootBullet()
        {
            //var newBullet = Instantiate(bullet,tower.gameObject.transform.position,Quaternion.identity);
            //newBullet.GetComponent<Bullet>().setDirection(tower.transform.position,enemy,4f);

            yield return new WaitForSeconds(1);
            StartCoroutine("shootBullet",1);
        }
    }
}
