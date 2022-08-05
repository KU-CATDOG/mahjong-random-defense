using System.Collections.Generic;
using UnityEngine;
namespace MRD
{
    public class JoinAnimator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform sourceParent;
        [SerializeField]
        private RectTransform targetParent;
        private float timer=0;
        private List<GameObject> sourceTowers = new();
        private GameObject targetTower;
        private (float,float) animationTime = new(0.25f, 0.05f);  // 모이는 시간, 떨어지는 시간
        private bool phase = false;
        private bool en = false;
        public void Init(List<GameObject> sourceTowers, GameObject targetTower)
        {
            GameObject targetClone = Instantiate(targetTower);
            targetClone.transform.SetParent(targetParent,false);
            targetClone.AddComponent<SimpleLifter>().Init(floatTime:animationTime.Item1, dropTime:animationTime.Item2);
            this.targetTower = targetClone;

            foreach(var tower in sourceTowers){
                GameObject clone = Instantiate(tower);
                clone.transform.SetParent(sourceParent,false);
                clone.AddComponent<SimpleMover>().Init(tower.GetComponent<RectTransform>().anchoredPosition, this.targetTower.GetComponent<RectTransform>().anchoredPosition, animationTime.Item1, animationTime.Item1);
                this.sourceTowers.Add(clone);
            }
            en = true;
        }
        private void Update()
        {
            if(!en) return;
            timer += Time.deltaTime;
            if(!phase)
            {
                if(timer > animationTime.Item1)
                {
                    phase = true;
                    timer = 0;
                }
                return;
            }

            if(timer > animationTime.Item2)
            {
                Destroy(gameObject);
            }
        }
        
    }
}