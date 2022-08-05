using UnityEngine;
using System.Collections.Generic;
namespace MRD
{
    class SimpleLifter : MonoBehaviour
    {
        private float timer = 0f;

        private (float,float) targetTime;
        private Vector3 targetScale;
        private Vector3 startScale;
        private Vector3 displacement;
        private bool en = false;
        private RectTransform rectTransform;
        private bool floatDrop = false;
        private GameObject lastChild;
        
        public void Init(float targetScale = 1.5f, float startScale = 1f, float floatTime=0.25f, float dropTime=0.05f)
        {
            rectTransform = GetComponent<RectTransform>();
            this.startScale = Vector3.one * startScale - new Vector3(0,0,1 - startScale);
            this.targetScale = Vector3.one * targetScale - new Vector3(0,0,1 - targetScale);
            this.targetTime = new(floatTime, dropTime);
            displacement = this.targetScale - this.startScale;
            var child = transform.GetChild(transform.childCount-1).gameObject;
            //child.SetActive(false);
            //lastChild = child;
            
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            
            if(!floatDrop){
                rectTransform.localScale = startScale + displacement * ((timer>targetTime.Item1 ? targetTime.Item1 : timer) / targetTime.Item1);
                if(timer > targetTime.Item1){
                    floatDrop = true;
                    timer = 0;
                    //lastChild.SetActive(true);
                }
                return;
            }
            rectTransform.localScale = targetScale - displacement * EaseOutCubic((timer>targetTime.Item2 ? targetTime.Item2 : timer) / targetTime.Item2);
            if(timer > targetTime.Item2)
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