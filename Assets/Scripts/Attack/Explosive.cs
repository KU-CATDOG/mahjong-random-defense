using UnityEngine;

namespace MRD
{
    public class Explosive : Attack
    {
        [SerializeField]
        private Sprite[] sprite;

        private readonly float animationTime = 0.15f;
        private float animationTimer;

        private readonly Color[] color =
        {
            new Color32(0xFF, 0xFF, 0xFF, 150), new Color32(0x66, 0xBB, 0x6A, 150), new Color32(0xE5, 0x39, 0x35, 150),
        }; // 백발중

        private float timer;
        private bool timerEnabled;

        //폭발 공격
        //폭발의 경우 탄환 데미지는 받지 않고 폭발 데미지만 받음
        //폭발 반경: (0.5 + 패 개수 *0.1)m
        //피해는 데미지만큼
        //폭발 색상 있음 백:하양, 발: 초록, 중: 빨강

        public ExplosiveInfo ExplosiveInfo => (ExplosiveInfo)attackInfo;

        private void Update()
        {
            animationTimer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if (animationTimer > animationTime) animationTimer = animationTime;
            transform.localScale = new Vector3(EaseOutCubic(animationTimer / animationTime) * ExplosiveInfo.Radius,
                EaseOutCubic(animationTimer / animationTime) * ExplosiveInfo.Radius, 1);
            if (timerEnabled)
                timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if (timer > 0.5f)
                Destroy(gameObject);
        }

        //color 0: 백, 1: 발, 2: 중
        protected override void OnInit()
        {
            gameObject.GetComponent<SpriteRenderer>().material.color =
                color[ExplosiveInfo.Type > 2 ? 2 : ExplosiveInfo.Type];
            var targets = Physics2D.OverlapCircleAll(ExplosiveInfo.Origin, ExplosiveInfo.Radius / 2);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].tag == "Enemy")
                    if (ExplosiveInfo.Target == null || targets[i].gameObject != ExplosiveInfo.Target.gameObject)
                        targets[i].gameObject.GetComponent<EnemyController>().OnHit(ExplosiveInfo);
            }

            // GetComponent<SpriteRenderer>().sprite = Sprite[Color];

            transform.position = ExplosiveInfo.Origin;
            transform.localScale = Vector2.one * ExplosiveInfo.Radius;

            timerEnabled = true;
            // Destroy(gameObject, 1);
        }

        private float EaseOutCubic(float t) // 0~1
            =>
                1 - Mathf.Pow(1 - t, 3);
    }
}
