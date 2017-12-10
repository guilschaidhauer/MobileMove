using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRotHolder : MonoBehaviour {

    public float initialRotation;
    private bool set;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start ()
    {
        set = false;
    }

    void Update()
    {
        if (!set)
        {
            Vector3 pos = new Vector3(MobileDetection.NormalizedFacePosition.x, MobileDetection.NormalizedFacePosition.y, MobileDetection.NormalizedFacePosition.z);

            if (pos.z != 0)
            {
                initialRotation = MobileDetection.NormalizedFacePosition.x;
                set = true;
            }
        }
    }
}
