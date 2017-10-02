using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;

public class OpenCVFaceDetection : MonoBehaviour
{
    public static List<Vector3> NormalizedFacePositions { get; private set; }

    /// <summary>
    /// Downscale factor to speed up detection.
    /// </summary>
    private const int DetectionDownScale = 1;

    private bool _ready;

    bool _threadRunning;
    Thread _thread;
    private static CvCircle theCircle;

    void Start()
    {
        Application.runInBackground = true;
        // Begin our heavy work on a new thread.
        _thread = new Thread(ThreadedWork);
        _thread.Start();
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

    void ThreadedWork()
    {
        _threadRunning = true;
        bool workDone = false;

        CvCircle[] _faces;


        int result = OpenCVInterop.Init();
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


        _faces = new CvCircle[2];
        NormalizedFacePositions = new List<Vector3>();
        _ready = true;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            if (!_ready)
                return;

            unsafe
            {
                fixed (CvCircle* outFaces = _faces)
                {
                    OpenCVInterop.RunServer(outFaces);
                }
            }

            //Debug.Log(_faces[0].X + " - " + _faces[0].Y + " - " + _faces[0].Radius);
            //NormalizedFacePositions.Clear();
            //NormalizedFacePositions.Add(new Vector2((_faces[i].X * DetectionDownScale) / CameraResolution.x, 1f - ((_faces[i].Y * DetectionDownScale) / CameraResolution.y)));
            theCircle.X = _faces[0].X;
            theCircle.Y = _faces[0].Y;
            theCircle.Radius = _faces[0].Radius;
        }
        _threadRunning = false;
    }
}

// Define the functions which can be called from the .dll.
internal static class OpenCVInterop
{
    [DllImport("Win32Project1")]
    internal static extern int Init();

    [DllImport("Win32Project1")]
    internal static extern int Close();

    [DllImport("Win32Project1")]
    internal unsafe static extern void RunServer(CvCircle* outFaces);
}

// Define the structure to be sequential and with the correct byte size (3 ints = 4 bytes * 3 = 12 bytes)
[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct CvCircle
{
    public int X, Y, Radius;
}