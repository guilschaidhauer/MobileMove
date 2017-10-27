using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroControl : MonoBehaviour
{
    public Text rotText;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        Debug.Log("true");
        gyro = Input.gyro;
        gyro.enabled = true;

        //cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        rot = new Quaternion(0, 0, 1, 0);

        return true;
    }

    private void Update()
    {
        //if (gyroEnabled)
        //{
            Vector4 myRot = new Vector4(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z, OpenCVFaceDetection.NormalizedFacePosition.w);

            Quaternion myGyro = new Quaternion(myRot.x, myRot.y, myRot.z, myRot.w);

            transform.localRotation = myGyro * rot;
            //rotText.text = gyro.attitude.ToString() + "  #######  " + (gyro.attitude * rot).ToString();
            //rotText.text = gyro.rotationRateUnbiased.x + " || " + gyro.rotationRateUnbiased.y + " || " + gyro.rotationRateUnbiased.z;
        //}
        //else
        //{
            //rotText.text = "fuck you";
        //}
    }
}