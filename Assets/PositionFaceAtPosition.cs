using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    public float offset;
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
        if (OpenCVFaceDetection.NormalizedFacePosition.x != 1 && OpenCVFaceDetection.NormalizedFacePosition.y != 1)
        {
            //Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance));
            Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);

            if (smooth)
            {
                Debug.Log(pos);

                //transform.localEulerAngles = new Vector3(pos.y, pos.x + offset, -pos.z);
                //transform.localEulerAngles = new Vector3(0, 0, -pos.z);
                transform.localEulerAngles = new Vector3(0, pos.x + 35, 0);
                //transform.localEulerAngles = new Vector3(pos.y - 10, 0, 0);
                //transform.localEulerAngles = new Vector3(pos.y - 10, 0, -pos.z);
            }
            else
            {
                //transform.position = pos;
            }
        }

        //Debug.Log(OpenCVFaceDetection.NormalizedFacePosition.x);
    }
}