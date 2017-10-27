using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    public bool smooth = true;
    public float smoothTime = 0.1F;
    public Vector3 velocity = Vector3.zero;

    private float _camDistance;

    public Transform target;
    public float speed;

    void Start()
    {
        Application.runInBackground = true;
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);
        //Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.z);


        //Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.z * -1, 0, 26);


        transform.rotation = Quaternion.Euler(pos);
    }
}