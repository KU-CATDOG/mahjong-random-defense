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
        public GameObject Target;
        private Collider2D[] targets;

        public float ExplosionRange { get; private set; }

        //color 0: 백, 1: 발, 2: 중
        public void SetExplosionData(TowerStat towerStat, int color, GameObject target)
        {
            TowerStat = towerStat;
            Color = color;
            Target = target;

            ExplosionRange = (float)(0.5 + TowerStat.Holder.TowerInfo.Hais.Count * 0.1);


            DealDamage();
        }

        public void DealDamage()
        {
            targets = Physics2D.OverlapCircleAll(Target.transform.position, ExplosionRange, 1<<3);
            
            for(int i = 0; i < targets.Length; i++)
            {
                Debug.Log(targets[i].name);
                if(targets[i].gameObject != Target)
                    targets[i].gameObject.GetComponent<EnemyController>().OnHit(TowerStat);
            }

            GetComponent<SpriteRenderer>().sprite = Sprite[Color];
            transform.position = Target.transform.position;
            transform.localScale = Vector2.one * ExplosionRange;
            Destroy(gameObject, 1);
        }

    }
}