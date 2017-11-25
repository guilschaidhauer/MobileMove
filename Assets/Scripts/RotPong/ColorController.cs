using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour {

    public Color blue;
    public Color green;

    private Material m_Material;
    private float initialRotation;
    private bool set;

    // Use this for initialization
    void Start ()
    {
        m_Material = GetComponent<Renderer>().material;
        m_Material.color = Color.green;
        set = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = new Vector3(MobileDetection.NormalizedFacePosition.x, MobileDetection.NormalizedFacePosition.y, MobileDetection.NormalizedFacePosition.z);

        if(!set && pos.z != 0)
        {
            initialRotation = MobileDetection.NormalizedFacePosition.x;
            set = true;
        }

        Debug.Log(pos.x);

        if (pos.x <= initialRotation && pos.x >= initialRotation - 25)
        {
            m_Material.color = blue;
        }
        else if (pos.x > initialRotation && pos.x <= initialRotation + 25)
        {
            m_Material.color = green;
        }
    }
}
