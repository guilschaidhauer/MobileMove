﻿using UnityEngine;

public class PositionFaceAtPosition : MonoBehaviour
{
    public bool smooth = true;
    public float smoothTime = 0.1F;
    public Vector3 velocity = Vector3.zero;

    private float _camDistance;

    private Vector3 lastPos;

    void Start()
    {
        Application.runInBackground = true;
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        lastPos = Vector3.one;
    }

    void Update()
    {
        Vector3 cvPos = new Vector3(OpenCVFaceDetection.NormalizedFacePosition.x, OpenCVFaceDetection.NormalizedFacePosition.y, OpenCVFaceDetection.NormalizedFacePosition.z);

        //if (cvPos == Vector3.zero)
        //{
        //    return;
        //}
            

        if (cvPos.x != 1 && cvPos.y != 1)
        {
            Vector3 pos;

            if (cvPos == Vector3.zero)
            {
                pos = Camera.main.ViewportToWorldPoint(new Vector3(lastPos.x, lastPos.y, _camDistance));
            }
            else
            {
                pos = Camera.main.ViewportToWorldPoint(new Vector3(cvPos.x, cvPos.y, _camDistance));
                lastPos = pos;
            }


            if (smooth)
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
            else
                transform.position = pos;
        }
    }
}