using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    private float _camDistance;

    void Start()
    {
        Application.runInBackground = true;
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        if (OpenCVFaceDetection.NormalizedFacePositions == null)
            return;
        if (OpenCVFaceDetection.NormalizedFacePositions.Count == 0)
            return;
   
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x, OpenCVFaceDetection.NormalizedFacePositions[0].y, _camDistance));
    }
}