using UnityEngine;

namespace MRD
{
    class SimpleMover : MonoBehaviour
    {
        private float timer = 0f;

        private float targetTime;
        private float destroyTime;

        private Vector3 targetPosition;
        private Vector3 startPosition;
        private Vector3 displacement;
        private bool en = false;
        private RectTransform rectTransform;
        
        public void Init(Vector3 startPosition, Vector3 targetPosition, float targetTime, float destroyTime)
        {
            rectTransform = GetComponent<RectTransform>();
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;
            this.targetTime = targetTime;
            this.destroyTime = destroyTime;
            displacement = targetPosition - startPosition;
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            rectTransform.anchoredPosition3D = startPosition + displacement * EaseOutCubic((timer>targetTime ? targetTime : timer) / targetTime);
            if(timer > destroyTime)
            {
                Destroy(gameObject);
                return;
            }
        }
        private float EaseOutCubic(float t) // 0~1
            =>
                1- Mathf.Pow(1 - t, 3);
    }
}