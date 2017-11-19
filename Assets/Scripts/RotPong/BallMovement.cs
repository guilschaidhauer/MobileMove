using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    private Rigidbody myRigidbody;
    private float lastX_force;
    private float lastY_force;

    private float lastDist;
    private float lastV;

    void Start ()
    {
        myRigidbody = this.GetComponent<Rigidbody>();

        lastX_force = 5f;
        lastY_force = 0f;
        lastDist = 0f;
        lastV = 0f;

        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            myRigidbody.velocity = new Vector3(-lastX_force, 0f);
        }
        else if (rand == 1)
        {
            myRigidbody.velocity = new Vector3(lastX_force, 0f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter (Collision hit)
    {
        float dist;

        if (hit.gameObject.tag == "Player")
        {
            dist = this.transform.position.y - hit.gameObject.transform.position.y;
            //lastY_force = dist * 1.5f;
            myRigidbody.velocity = new Vector3(5f, dist);
            lastDist = dist;
            lastV = 5f;
        }
        else if (hit.gameObject.name == "Wall")
        {
            myRigidbody.velocity = new Vector3(-5f, lastDist);
            lastV = -5f;
            //lastY_force = -lastY_force;
        }
        else if (hit.gameObject.name == "Wall1" || hit.gameObject.name == "Wall2")
        {
            myRigidbody.velocity = new Vector3(lastV, -lastDist);
            lastDist = -lastDist;
            // lastX_force = -lastX_force;
        }
    } 
}
