using UnityEngine;

public class RotateToPhoneRotation : MonoBehaviour
{
    public bool adjust = false;
    public float adjustRate;
    public float adjustInterval;
    public float limit = 65;
    public bool weird = false;
    public Transform other;
    private bool rotating = true;

    private float cumulativeAdjustment;

    float acceleration;

    public void Start()
    {
        if (adjust)
        {
            InvokeRepeating("AdjustAxis", adjustInterval, adjustInterval);
        }
    }

    void AdjustAxis()
    {
        cumulativeAdjustment += adjustRate;
    }

    public void Update()
    {
        Vector4 pos = new Vector4(MobileDetection2.NormalizedFacePosition.x / 10000f, MobileDetection2.NormalizedFacePosition.y / 10000f, MobileDetection2.NormalizedFacePosition.z / 10000f, MobileDetection2.NormalizedFacePosition.w / 10000f);

        if (adjust)
            pos.x += cumulativeAdjustment;

        Debug.Log(pos);

        //Vector3 newPos = new Vector3(pos.z, pos.x, pos.y);

        //Quaternion myQuaternion = new Quaternion(pos.x, pos.y, pos.z, pos.w);
        //Quaternion myQuaternion = new Quaternion(0, -pos.z, 0, pos.w);
        //Quaternion myQuaternion = new Quaternion(-pos.x, 0, 0, pos.w);
        //Quaternion myQuaternion = new Quaternion(0, 0, -pos.y, pos.w);

        Quaternion myQuaternion = new Quaternion(-pos.x, -pos.z, -pos.y, -pos.w);

        //transform.localRotation = Quaternion.Euler(newPos);
        transform.localRotation = myQuaternion;
    }
}
