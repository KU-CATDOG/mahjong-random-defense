using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{

    public class Timer : MonoBehaviour
    {
        private float timer;
        private float targetTime;
        private int count;
        private int targetCount;
        private bool isRunning;
        public delegate IEnumerator OnTick(Tower tower);
        private OnTick onTick;
        private Tower coroutineOwner;
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

        // Update is called once per frame
        void Update()
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
    }
}