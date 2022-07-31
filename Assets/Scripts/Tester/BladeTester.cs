using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MRD
{
    public class BladeTester : MonoBehaviour
    {
        [SerializeField]
        public GameObject bladePrefab;
        public EnemyController enemy;

        //private BladeInfo b1 = new BladeInfo(enemy, enemy.transform.position, new TowerStat(null), bladePrefab.transform.position,"\0");

        [ContextMenu("칼질테스트")]
        private void testfunc()
        {
            BladeInfo b1 = new BladeInfo(enemy, enemy.transform.position, new TowerStat(null), bladePrefab.transform.position, AttackImage.Blade);

            var tmp = Instantiate(bladePrefab).GetComponent<Blade>();
            Debug.Log("Hello1");
            tmp.Init(b1);
            Debug.Log("Hello2");
        }
    }
}
