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
        /*if (OpenCVFaceDetection.NormalizedFacePositions == null)
            return;
        if (OpenCVFaceDetection.NormalizedFacePositions.Count == 0)
            return;*/

        Debug.Log(OpenCVFaceDetection.NormalizedFacePosition.x + " |||| " + OpenCVFaceDetection.NormalizedFacePosition.y);
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePositions[0].x, OpenCVFaceDetection.NormalizedFacePositions[0].y, _camDistance));
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance));
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(350, 142, 66/10));
    }
}