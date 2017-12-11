using UnityEngine;

public class RotateToPhoneRotation : MonoBehaviour
{
    public bool adjust = false;
    public float adjustRate;
    public float adjustInterval;
    public float limit = 65;
    private bool rotating = true;

    private float cumulativeAdjustment;

    public void Start ()
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

        if (adjust)
            pos.x += cumulativeAdjustment;

        //Vector3 newPos = new Vector3(pos.z * -1, 0, 0);
        //Vector3 newPos = new Vector3(0, 0, pos.y);

        ///Vector3 newPos = new Vector3(pos.z * -1, 0, pos.y * -1);

        //Vector3 newPos = new Vector3(pos.z, pos.x, pos.y);

        /*if (pos.y > limit)
            pos.y = limit;
        else if (pos.y < -limit)
            pos.y = -limit;*/

        Vector3 newPos = new Vector3(pos.z, pos.x, pos.y);
        //Vector3 newPos = new Vector3(0, 0, pos.x);

        transform.localRotation = Quaternion.Euler(newPos);
    }
}