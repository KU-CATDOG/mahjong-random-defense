using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD{
    public class CameraShake : MonoBehaviour
    {
        CameraShake Camera;
        Vector3 Cpos = new Vector3(5.0f, 6.0f, -10.0f);
        //float magnitude = 1.0f;
       
        // Start is called before the first frame update
        void Start()
        {
            Camera = GetComponent<CameraShake>();
        }

        public IEnumerator Shake(float time, float magnitude)
        {
            float timecheck = 0;
            while (timecheck <= time)
            {
                Camera.transform.position += (Vector3)Random.insideUnitCircle * magnitude;

                magnitude -= 0.001f;
                timecheck += Time.deltaTime;

                yield return null;
            }
            Camera.transform.position = Cpos;
        }
    }
}

