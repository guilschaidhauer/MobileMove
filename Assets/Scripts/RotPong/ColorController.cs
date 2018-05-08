using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour {

    public Color blue;
    public Color green;
    public bool isBlue;
    public InitialRotHolder myInitialRotHolder;
    public Transform phone;

    private Material m_Material;
    private float initialRotation;
    private bool set;

    // Use this for initialization
    void Start ()
    {
        m_Material = GetComponent<Renderer>().material;
        set = false;
        isBlue = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = phone.localRotation.eulerAngles;

        //Debug.Log(pos.x);

        if (!set && pos.z != 0)
        {
            initialRotation = pos.x;
            //initialRotation = myInitialRotHolder.initialRotation;
            set = true;
        }

        //if (pos.x <= initialRotation && pos.x >= initialRotation - 25)
        //if (pos.x - initialRotation <= 0)
        if (pos.x < 40)
        {
            m_Material.color = Color.blue;
            isBlue = true;
        }
        //else if (pos.x > initialRotation && pos.x <= initialRotation + 25)
        else
        {
            m_Material.color = Color.green;
            isBlue = false;
        }
    }
}
