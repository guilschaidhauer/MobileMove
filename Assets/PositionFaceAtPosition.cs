using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    public bool smooth = true;
    public float smoothTime = 0.1F;
    public Vector3 velocity = Vector3.zero;

    private float _camDistance;

    void Start()
    {
        Application.runInBackground = true;
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        /*if (OpenCVFaceDetection.NormalizedFacePositions == null)
            return;
        if (OpenCVFaceDetection.NormalizedFacePositions.Count == 0)
            return;*/

        Debug.Log(OpenCVFaceDetection.NormalizedFacePosition.x + " |||| " + OpenCVFaceDetection.NormalizedFacePosition.y);
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x, OpenCVFaceDetection.NormalizedFacePositions[0].y, _camDistance));
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance));
        //if (OpenCVFaceDetection.NormalizedFacePosition != Vector3.zero)
        //transform.Translate(Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance)), Space.World);
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(350, 142, 66/10));

        if (OpenCVFaceDetection.NormalizedFacePosition.x != 1 && OpenCVFaceDetection.NormalizedFacePosition.y != 1)
        {
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance));
            if (smooth)
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
            else
                transform.position = pos;
        }
    }
}