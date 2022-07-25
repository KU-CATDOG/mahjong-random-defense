using UnityEngine;

namespace MRD
{
    public class Explosive : Attack
    {
        [SerializeField]
        private Sprite[] sprite;

        //폭발 공격
        //폭발의 경우 탄환 데미지는 받지 않고 폭발 데미지만 받음
        //폭발 반경: (0.5 + 패 개수 *0.1)m
        //피해는 데미지만큼
        //폭발 색상 있음 백:하양, 발: 초록, 중: 빨강

        public ExplosiveInfo ExplosiveInfo => (ExplosiveInfo)attackInfo;
        private float timer = 0f;
        private bool timerEnabled = false;
        private Color[] color = new Color[] {new Color32(0xFF,0xFF,0xFF,150),new Color32(0x66,0xBB,0x6A,150),new Color32(0xE5,0x39,0x35,150)}; // 백발중

        //color 0: 백, 1: 발, 2: 중
        protected override void OnInit()
        {
            transform.localScale = new Vector3(ExplosiveInfo.Radius, ExplosiveInfo.Radius, 1);
            gameObject.GetComponent<SpriteRenderer>().material.color = color[ExplosiveInfo.Type > 2 ? 2 : ExplosiveInfo.Type];
            var targets = Physics2D.OverlapCircleAll(ExplosiveInfo.Origin, ExplosiveInfo.Radius, 1 << 3);

            for (int i = 0; i < targets.Length; i++)
            {
                Debug.Log(targets[i].name);

                if (targets[i].gameObject != ExplosiveInfo.Target.gameObject)
                    targets[i].gameObject.GetComponent<EnemyController>().OnHit(ExplosiveInfo);
            }

            // GetComponent<SpriteRenderer>().sprite = Sprite[Color];

            transform.position = ExplosiveInfo.Origin;
            transform.localScale = Vector2.one * ExplosiveInfo.Radius;

            timerEnabled = true;
            // Destroy(gameObject, 1);
        }

        void Update()
        {
            if(timerEnabled == true)
                timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if(timer > 0.5f)
                Destroy(gameObject);
        }
    }
}