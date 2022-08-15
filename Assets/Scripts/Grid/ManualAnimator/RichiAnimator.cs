using UnityEngine;

namespace MRD
{
    class RiChiAnimator : MonoBehaviour
    {
        private (float,float) scaleRange;
        private float animationTime;
        float timer = 0f;
        bool en = false;
        Transform target;
        
        public void Init(float startScale = 0f, float endScale = 1f, float animationTime = 0.5f)
        {
            scaleRange = new(startScale,endScale);
            this.animationTime = animationTime;
            target = transform;
            transform.localScale = Vector3.one * startScale;
            (transform as RectTransform).pivot = new Vector2(0.2f,0.8f);
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            target.localScale = Vector3.one * scaleRange.Item1 + Vector3.one * (scaleRange.Item2 - scaleRange.Item1) * EaseOutBack(timer / animationTime);
            if(timer > animationTime)
            {
                Destroy(this);
                return;
            }
        }
        private void OnDestroy()
        {
            (transform as RectTransform).pivot = new Vector2(0.5f,0.5f);
        }
        private float EaseOutBack(float t) 
            => 1 + 2.70158f * Mathf.Pow(t - 1, 3) + 1.70158f * Mathf.Pow(t - 1, 2);
        
    }
}