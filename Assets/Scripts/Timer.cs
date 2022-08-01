using System.Collections;
using UnityEngine;

namespace MRD
{
    public class Timer : MonoBehaviour
    {
        public delegate IEnumerator OnTick(Tower tower);

        private Tower coroutineOwner;
        private int count;
        private bool isRunning;
        private OnTick onTick;
        private int targetCount;
        private float targetTime;
        private float timer;

        // Update is called once per frame
        private void Update()
        {
            if (!isRunning) return;
            timer += Time.deltaTime * RoundManager.Inst.playSpeed;
            if (timer < targetTime) return;
            timer = 0f;
            count++;
            coroutineOwner.StartCoroutine(onTick(coroutineOwner));
            if (count >= targetCount)
            {
                isRunning = false;
                Destroy(gameObject);
            }
        }

        public void Init(float targetTime, int targetCount, Tower coroutineOwner, OnTick onTick)
        {
            this.targetTime = targetTime;
            this.targetCount = targetCount;
            this.onTick = onTick;
            this.coroutineOwner = coroutineOwner;
            timer = 0f;
            count = 0;
            isRunning = true;
        }
    }
}
