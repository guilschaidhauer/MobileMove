using UnityEngine;

public class RotateToPhoneRotation : MonoBehaviour
{
    public bool adjust = false;
    public float adjustRate;
    public float adjustInterval;
    public float limit = 65;
    private bool rotating = true;

    private float cumulativeAdjustment;

    private float lastCheckpoint;
    private bool hitLimit;
    private bool downLeft;

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

        float acceleration = pos.x;

        if (acceleration > 0.5f)
        {
            Debug.Log("->");
        }
        else if (acceleration < -0.5f)
        {
            Debug.Log("<-");
        }

        if (adjust)
            pos.x += cumulativeAdjustment;

        //Vector3 newPos = new Vector3(pos.z, pos.x, pos.y);
        Vector3 newPos = new Vector3(0, 0, pos.y);

        //Debug.Log(newPos.z);

        transform.localRotation = Quaternion.Euler(newPos);
    }
}