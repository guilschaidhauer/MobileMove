using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;

public class MobileDetection2 : MonoBehaviour {
    public bool destroy = false;
    public static List<Vector4> NormalizedFacePositions { get; private set; }
    public static Vector4 NormalizedFacePosition { get; private set; }


    /// <summary>
    /// Downscale factor to speed up detection.
    /// </summary>
    private const int DetectionDownScale = 1;

    private bool _ready;

    bool _threadRunning;
    Thread _thread;
    private static AndroidQuaternion theCircle;

    void Start()
    {
        Application.runInBackground = true;
        NormalizedFacePosition = new Vector2(0, 0);
        // Begin our heavy work on a new thread.
        _thread = new Thread(ThreadedWork);
        _thread.Start();
    }

    void OnApplicationQuit()
    {
        if (_ready)
        {
            SocketsInterop2.Close();
        }
    }

    void Update()
    {
        //Debug.Log(theCircle.X + " - " + theCircle.Y + " - " + theCircle.Radius);
    }

    void ThreadedWork()
    {
        _threadRunning = true;
        bool workDone = false;

        AndroidQuaternion[] _faces;


        int result = SocketsInterop2.Init();
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


        _faces = new AndroidQuaternion[2];
        NormalizedFacePositions = new List<Vector4>();
        _ready = true;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            if (!_ready)
                return;

            unsafe
            {
                fixed (AndroidQuaternion* outFaces = _faces)
                {
                    SocketsInterop2.RunServer(outFaces);
                }
            }

            theCircle.X = _faces[0].X;
            theCircle.Y = _faces[0].Y;
            theCircle.Z = _faces[0].Z;
            theCircle.Radius = _faces[0].Radius;

            //NormalizedFacePosition = new Vector2(((float)(640 - _faces[0].X) * DetectionDownScale) / 640f, 1f - (((float)_faces[0].Y * DetectionDownScale) / 480f));
            NormalizedFacePosition = new Vector4(theCircle.X, theCircle.Y, theCircle.Z, theCircle.Radius);
        }
        _threadRunning = false;
    }

    void OnDisable()
    {
        OnApplicationQuit();
        //_threadRunning = false;
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

// Define the structure to be sequential and with the correct byte size (3 ints = 4 bytes * 3 = 12 bytes)
[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct AndroidQuaternion
{
    public int X, Y, Z, Radius;
}

// Define the functions which can be called from the .dll.
internal static class SocketsInterop2
{
    [DllImport("Win32Project3")]
    internal static extern int Init();

    [DllImport("Win32Project3")]
    internal static extern int Close();

    [DllImport("Win32Project3")]
    internal unsafe static extern void RunServer(AndroidQuaternion* outFaces);
}