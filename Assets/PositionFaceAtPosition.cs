using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    private bool rotating = true;

    public void Update()
    {
        Vector3 pos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);
        //Vector3 newPos = new Vector3(pos.z * -1, 0, 0);
        //Vector3 newPos2 = new Vector3(0, 0, pos.y * -1);

        Vector3 newPos = new Vector3(pos.z * -1, 0, pos.y * -1);

        transform.localRotation = Quaternion.Euler(newPos);
    }
}