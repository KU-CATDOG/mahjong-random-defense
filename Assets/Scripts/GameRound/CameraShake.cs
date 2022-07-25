using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD{
    public class CameraShake : MonoBehaviour
    {
        CameraShake Camera;
        Vector3 Cpos;
        //float magnitude = 1.0f;
       
        // Start is called before the first frame update
        void Start()
        {
            Camera = GetComponent<CameraShake>();
            //Cpos = Camera.transform.position;
        }

        public void SetCameraPosition()
        {
            Cpos = Camera.transform.position;
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

