using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    public bool smooth = true;
    public bool useX = true;
    public bool useY = true;
    public bool useZ = true;
    public float zOffset;
    public float smoothTime = 0.1F;
    public Vector3 velocity = Vector3.zero;

    private float _camDistance;

    private Vector3 lastPos;
    private Vector3 startPos;

    private Material m_Material;
    private GameObject lightSphere;

    void Awake ()
    {
        if (transform.childCount > 0)
        {
            lightSphere = transform.GetChild(0).gameObject;
            m_Material = lightSphere.GetComponent<Renderer>().material;
            m_Material.color = Color.green;
        }

        lastPos = Vector3.one;
        startPos = transform.position;
    }

    void Start()
    {
        Application.runInBackground = true;
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        Vector3 cvPos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);
        int on = (int)OpenCVFaceDetection.NormalizedFacePosition.w;

        Debug.Log(OpenCVFaceDetection.NormalizedFacePosition);

        //if (OpenCVFaceDetection.NormalizedFacePosition.w == 0)
        //    m_Material.color = Color.green;
        //else if (OpenCVFaceDetection.NormalizedFacePosition.w == 1)
        //    m_Material.color = Color.yellow;

        cvPos.z = cvPos.z / 20f + zOffset;

        if (cvPos.x != 1 && cvPos.y != 1)
        {
            Vector3 pos;

            if (cvPos == Vector3.zero)
            {
                //pos = Camera.main.ViewportToWorldPoint(new Vector3(lastPos.x, lastPos.y, _camDistance));
                pos = Camera.main.ViewportToWorldPoint(new Vector3(lastPos.x, lastPos.y, lastPos.z));
            }
            else
            {
                //pos = Camera.main.ViewportToWorldPoint(new Vector3(cvPos.x, cvPos.y, _camDistance));
                pos = Camera.main.ViewportToWorldPoint(new Vector3(cvPos.x, cvPos.y, cvPos.z));
                lastPos = pos;
            }

            if (smooth)
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
            else
                transform.position = pos;

            BlockAxis();
        }
    }

    void BlockAxis ()
    {
        Vector3 pos = transform.position;

        if (!useX)
        {
            pos.x = startPos.x;
        }

        if (!useY)
        {
            pos.y = startPos.y;
        }

        if (!useZ)
        {
            pos.z = startPos.z;
        }

        transform.position = pos;
    }
}