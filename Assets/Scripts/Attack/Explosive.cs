using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class Explosive : Attack
    {
        [SerializeField]
        private Sprite[] Sprite;

        //폭발 공격
        //폭발의 경우 탄환 데미지는 받지 않고 폭발 데미지만 받음
        //폭발 반경: (0.5 + 패 개수 *0.1)m
        //피해는 데미지만큼
        //폭발 색상 있음 백:하양, 발: 초록, 중: 빨강


        private TowerStat TowerStat;
        public int Color;
        public GameObject Collision;
        private Collider2D[] targets;

        public float ExplosionRange { get; private set; }

        //color 0: 백, 1: 발, 2: 중
        public void SetExplosionData(TowerStat towerStat, int color, GameObject collision)
        {
            TowerStat = towerStat;
            Color = color;
            Collision = collision;

            ExplosionRange = (float)(0.5 + TowerStat.Holder.TowerInfo.Hais.Count * 0.1);

            Sprite = Resources.LoadAll<Sprite>("ExplosionEffect/Explosion");
        }

        public void DealDamage()
        {
            targets = Physics2D.OverlapCircleAll(Collision.transform.position, ExplosionRange, 3);
            
            //remove
            //deal
            for(int i = 0; i < targets.Length; i++)
            {
                if(targets[i].gameObject != Collision)
                    targets[i].gameObject.GetComponent<EnemyController>().OnHit(TowerStat);
            }
            //effect
            if (Color == 0)
                Instantiate(Sprite[1], Collision.transform.position, Quaternion.identity);

            if(Color == 1)
                Instantiate(Sprite[2], Collision.transform.position, Quaternion.identity);

            if(Color == 2)
                Instantiate(Sprite[3], Collision.transform.position, Quaternion.identity);

        }

    }
}