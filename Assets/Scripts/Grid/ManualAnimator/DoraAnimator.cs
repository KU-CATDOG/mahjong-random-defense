using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class DoraAnimator : MonoBehaviour
    {
        private float timer = 0f;
        private bool phase = false;
        private HaiType haiType;
        private int number;
        private RectTransform rt;
        private RectTransform stampRt;
        private (float,float,float) animationTime = new(0.7f, 0.4f, 0.3f);  // 총 찍는 시간, 찍는 시간 중 쉬는 시간, 없어지는 시간
        private Vector3 startPos;
        private Vector3 targetPos;
        bool en = false;
        public void Init(HaiType haiType, int number, Vector3 targetLocation)
        {
            this.haiType = haiType;
            this.number = haiType is HaiType.Kaze or HaiType.Sangen
                ? number + 1
                : number;
            rt = (RectTransform) transform;
            rt.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = Tower.SingleMentsuSpriteDict[haiType + this.number.ToString()];
            stampRt = (RectTransform) rt.GetChild(2);
            startPos = rt.anchoredPosition;
            targetPos = targetLocation;
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            if(!phase)
            {
                stampRt.localScale = Vector3.one + Vector3.one * 1f * (1f-EaseOutCubic(timer / (animationTime.Item1-animationTime.Item2)));
                if(timer > animationTime.Item1)
                {
                    phase = true;
                    timer = 0f;
                }
                return;
            } 
            rt.anchoredPosition = startPos+(targetPos-startPos)*EaseOutCubic(timer / animationTime.Item2);
            rt.localScale = Vector3.one * (1f-EaseOutCubic(timer / animationTime.Item3));

            if(timer > animationTime.Item2)
            {
                Destroy(gameObject);
            }
        }
        private float EaseOutCubic(float t) 
            =>
                1 - Mathf.Pow(1 - (t>1f?1f:t), 3);
    }
}