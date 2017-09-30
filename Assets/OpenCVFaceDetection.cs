using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading; 

public class OpenCVFaceDetection : MonoBehaviour
{
    public static List<Vector2> NormalizedFacePositions { get; private set; }
    public static Vector2 CameraResolution;

    private static bool _ready;
    private static CvCircle theCircle;

    bool _threadRunning;
    Thread _thread;

    void Start()
    {
        // Begin our heavy work on a new thread.
        _thread = new Thread(ThreadedWork);
        _thread.Start();
    }

    void ThreadedWork()
    {
        _threadRunning = true;
        bool workDone = false;

        int DetectionDownScale = 1;

        int _maxFaceDetectCount = 5;
        CvCircle[] _faces;

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

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            //theCircle.X = 80;
            //theCircle.Y = 80;
            //theCircle.Radius = 80;

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
                NormalizedFacePositions.Add(new Vector2((_faces[i].X * DetectionDownScale) / CameraResolution.x, 1f - ((_faces[i].Y * DetectionDownScale) / CameraResolution.y)));
            }
        }
        _threadRunning = false;
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

    void OnDisable()
    {
        // If the thread is still running, we should shut it down,
        // otherwise it can prevent the game from exiting correctly.
        if (_threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            _threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            _thread.Join();
        }

        // Thread is guaranteed no longer running. Do other cleanup tasks.
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