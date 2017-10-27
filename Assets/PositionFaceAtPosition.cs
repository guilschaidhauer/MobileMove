using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    private Quaternion rot;
    private Quaternion myGyro;

    void Start()
    {
        Application.runInBackground = true;
        //rot = new Quaternion(1, 1, 0, 0);
    }

    void Update()
    {
        Vector4 myRot = new Vector4(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z, OpenCVFaceDetection.NormalizedFacePosition.w);

        //Quaternion myGyro = new Quaternion(myRot.x, myRot.y, myRot.z, myRot.w);
        //Quaternion myGyro = new Quaternion(myRot.x * -1, 0, 0, myRot.w);
        //Quaternion myGyro = new Quaternion(0, myRot.y * -1, 0, myRot.w);
        //Quaternion myGyro = new Quaternion(0, 0, myRot.z, myRot.w);
        Quaternion myGyro = new Quaternion(myRot.x * -1, myRot.y * -1, 0, myRot.w);


        //transform.localRotation = myGyro * rot;

        transform.localRotation = myGyro;

        Debug.Log(myRot);
    }
}