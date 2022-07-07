using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void ModifyCameraScale()
    {
        Camera cam = GetComponent<Camera>();
        float scaleHeight = Screen.height / Screen.width;
        if (scaleHeight >= 2f)
        {
            float camHeight = 5f * scaleHeight;
            cam.orthographicSize = camHeight;
            transform.position = new Vector3(5f, 16f - camHeight, -10f);
        }
        else
        {
            cam.orthographicSize = 10f;
            transform.position = new Vector3(5f, 6f, -10f);
        }
    }
    private void Start()
    {
        ModifyCameraScale();
    }
}
