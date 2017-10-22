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
        if (OpenCVFaceDetection.NormalizedFacePosition.x != 1 && OpenCVFaceDetection.NormalizedFacePosition.y != 1)
        {
            //Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, _camDistance));
            Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);

            if (smooth)
            {
                //transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
                Vector3 targetDir = pos - transform.position;
                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                Debug.DrawRay(transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation(newDir);
                Debug.Log(pos);
                //transform.Rotate(pos);
            }
            else
            {
                //transform.position = pos;
            }
        }

        //Debug.Log(OpenCVFaceDetection.NormalizedFacePosition.x);
    }
}