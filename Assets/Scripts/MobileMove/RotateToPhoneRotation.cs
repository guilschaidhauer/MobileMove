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
        Vector3 pos = new Vector3(MobileDetection.NormalizedFacePosition.x, MobileDetection.NormalizedFacePosition.y, MobileDetection.NormalizedFacePosition.z);

        if (pos.x <= 15000)
        {
            acceleration = 1;
            pos.x = pos.x - 10000;
        }
        else {
            acceleration = 2;
            pos.x = pos.x - 20000;
        }

        if (weird)
        {
            pos.x = acceleration;
        }
        else if (!weird && other)
        {
            //pos.y = 360 - other.localRotation.eulerAngles.z;
            //pos.y *= -1;
            //pos.y = other.localRotation.eulerAngles.z;
        }

        Debug.Log(acceleration);
        Debug.Log(pos.z);

        if (adjust)
            pos.x += cumulativeAdjustment;

        Vector3 newPos = new Vector3(pos.z, pos.x, pos.y);
        //Vector3 newPos = new Vector3(0, 0, pos.y);

        //Debug.Log(newPos.z);

        transform.localRotation = Quaternion.Euler(newPos);
    }
}
