using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 camPos;
    private void Start()
    {
        ModifyCameraScale();
    }

    private void ModifyCameraScale()
    {
        var cam = GetComponent<Camera>();
        float scaleHeight = (float)Screen.height / Screen.width;
        if (scaleHeight >= 2f)
        {
            float camHeight = 5.2f * scaleHeight;
            cam.orthographicSize = camHeight;
            camPos = new Vector3(5f, 16f - camHeight, -10f);
            transform.position = camPos;
        }
        else
        {
            cam.orthographicSize = 10f;
            camPos = new Vector3(5f, 6f, -10f);
            transform.position = camPos;
        }
    }

    public IEnumerator Shake(float time, float magnitude)
    {
        float timecheck = 0;
        while (timecheck <= time)
        {
            float randomAngle = Random.Range(0f, 2 * Mathf.PI);
            transform.position = camPos + new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle), 0) * magnitude * (time - timecheck) / time;
            timecheck += Time.deltaTime;

            yield return null;
        }

        transform.position = camPos;
    }
}
