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

        Debug.Log(pos);

        if (adjust)
            pos.x += cumulativeAdjustment;

        //This is where I create a quaterion out of the values I get from the android phone
        Quaternion myQuaternion = new Quaternion(-pos.x, -pos.y, pos.z, pos.w);


        //This is where I set the location rotation of the object to the "adjusted" quaterion
        transform.localRotation = myQuaternion;
    }
}

























/*
 *       This is where I use the values I got from the Android phone to create a quaternion
         Quaternion myQuaternion = new Quaternion(pos.x, pos.y, pos.z, pos.w);
         Quaternion myQuaternion = new Quaternion(0, -pos.z, 0, pos.w);    
         Quaternion myQuaternion = new Quaternion(-pos.x, 0, 0, pos.w);    

        //Quaternion myQuaternion = new Quaternion(-pos.x, -pos.z, -pos.y, pos.w);

        //Quaternion myQuaternion = new Quaternion(pos.x, pos.z, pos.y, -pos.w);

        //transform.localRotation = Quaternion.Euler(newPos);
        //transform.localRotation = myQuaternion;

        transform.localRotation = myQuaternion;

        //Vector3 inEuler = myQuaternion.eulerAngles;

        //inEuler = new Vector3(inEuler.x, 0, 0);

        //Quaternion inQuaternion = Quaternion.Euler(inEuler);

        //transform.localRotation = inQuaternion;
*/