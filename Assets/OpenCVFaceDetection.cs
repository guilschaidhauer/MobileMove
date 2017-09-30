using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class OpenCVFaceDetection : MonoBehaviour
{
    public static List<Vector2> NormalizedFacePositions { get; private set; }
    public static Vector2 CameraResolution;

    /// <summary>
    /// Downscale factor to speed up detection.
    /// </summary>
    private const int DetectionDownScale = 1;

    private bool _ready;
    private int _maxFaceDetectCount = 5;
    private CvCircle[] _faces;
    private static CvCircle theCircle;

    void Start()
    {
        int camWidth = 0, camHeight = 0;
        int result = OpenCVInterop.Init(ref camWidth, ref camHeight);
        if (result < 0)
        {
            if (result == -1)
            {
                Debug.LogWarningFormat("[{0}] Failed to find cascades definition.", GetType());
            }
            else if (result == -2)
            {
                Debug.LogWarningFormat("[{0}] Failed to open camera stream.", GetType());
            }

            return;
        }

   
       CameraResolution = new Vector2(camWidth, camHeight);
        _faces = new CvCircle[_maxFaceDetectCount];
        NormalizedFacePositions = new List<Vector2>();
        OpenCVInterop.SetScale(DetectionDownScale);
        _ready = true;

        StartCoroutine("RandomizeFaces");
    }

    void OnApplicationQuit()
    {
        if (_ready)
        {
            OpenCVInterop.Close();
        }
    }

    void Update()
    {
        Debug.Log(theCircle.X + " - " + theCircle.Y + " - " + theCircle.Radius);
    }

    void Foo()
    {
        if (!_ready)
            return;

        int detectedFaceCount = 0;

       unsafe
        {
            fixed (CvCircle* outFaces = _faces)
            {
                OpenCVInterop.Detect(outFaces, _maxFaceDetectCount, ref detectedFaceCount);
            }
        }

        NormalizedFacePositions.Clear();
        for (int i = 0; i < 1; i++)
        {
            theCircle.X = _faces[i].X;
            theCircle.Y = _faces[i].Y;
            theCircle.Radius = _faces[i].Radius;
        }
    }

    IEnumerator RandomizeFaces ()
    {
        float duration = Time.time + 3.0f;
        while (true)
        {
            Foo();
            yield return null;
        }
    }
}

// Define the functions which can be called from the .dll.
internal static class OpenCVInterop
{
    [DllImport("Win32Project1")]
    internal static extern int Init(ref int outCameraWidth, ref int outCameraHeight);

    [DllImport("Win32Project1")]
    internal static extern void StartThread();

    [DllImport("Win32Project1")]
    internal static extern void UpdateFaces();

    [DllImport("Win32Project1")]
    internal static extern int Close();

    [DllImport("Win32Project1")]
    internal static extern int SetScale(int downscale);

    [DllImport("Win32Project1")]
    internal unsafe static extern void Detect(CvCircle* outFaces, int maxOutFacesCount, ref int outDetectedFacesCount);
}

// Define the structure to be sequential and with the correct byte size (3 ints = 4 bytes * 3 = 12 bytes)
[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct CvCircle
{
    public int X, Y, Radius;
}