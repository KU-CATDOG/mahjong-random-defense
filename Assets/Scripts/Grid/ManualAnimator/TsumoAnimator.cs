using UnityEngine;

namespace MRD
{
    class TsumoAnimator : MonoBehaviour
    {
        private (float,float) scaleRange;
        private float animationTime;
        float timer = 0f;
        bool en = false;
        Transform target;
        
        public void Init(float startScale = 1.5f, float endScale = 1f, float animationTime = 0.2f)
        {
            scaleRange = new(startScale,endScale);
            this.animationTime = animationTime;
            target = transform.GetChild(0);
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            target.localScale = Vector3.one * scaleRange.Item1 + Vector3.one * (scaleRange.Item2 - scaleRange.Item1) * EaseOutCubic(timer / animationTime);
            if(timer > animationTime)
            {
                Destroy(this);
                return;
            }
        }
        private float EaseOutCubic(float t)// 0~1
            => 1f - Mathf.Pow(1f - t, 3f);
        
    }
}