using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpheresManager : MonoBehaviour {

    public int lives;
    public float minSpeed;
    public float maxSpeed;
    public float intervalTime;

    public Transform xMax;
    public Transform xMin;
    public Transform yMax;
    public Transform yMin;
    public Transform bottomLimit;

    public Transform player;

    public GameObject sphere;

    public Text catchedText;

    private List<GameObject> spheres;
    private List<float> speeds;

    private bool gameOn;

    private int catchedSpheres = 0;

    private float lastCreationTime = 0;

    // Use this for initialization
    void Start ()
    {
        spheres = new List<GameObject>();
        speeds = new List<float>();

        for (int i = 0; i < 3; i++)
        {
            CreateSphere();
        }

        //InvokeRepeating("CreateSphere", intervalTime, intervalTime);

        gameOn = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOn)
        {
            MoveSpheres();
        }

        if (Time.time >= lastCreationTime + intervalTime)
        {
            CreateSphere();
            lastCreationTime = Time.time;
        }
    }

    void CreateSphere ()
    {
        if (gameOn)
        {
            spheres.Add(Instantiate(sphere, GetPosWithingBoundaries(), transform.rotation) as GameObject);
            speeds.Add(Random.Range(minSpeed, maxSpeed));
        }
    }

    Vector3 GetPosWithingBoundaries ()
    {
        Vector3 pos = new Vector3(0, 0, 0);

        pos.x = Random.Range(xMin.position.x, xMax.position.x);

        pos.y = Random.Range(yMin.position.y, yMax.position.y);

        return pos;
    }

    void MoveSpheres ()
    {
        for (int i=0; i<spheres.Count; i++)
        {
            Vector3 spherePos = spheres[i].transform.position;
            spheres[i].transform.position = new Vector3(spherePos.x, spherePos.y - speeds[i], spherePos.z);

            if (checkCollision(player.position, spheres[i].transform.position))
            {
                catchedSpheres++;
                catchedText.text = catchedSpheres.ToString();

                if (catchedSpheres >= 20)
                {
                    minSpeed = 0.05f;
                    maxSpeed = 0.08f;
                }
                else if (catchedSpheres >= 70)
                {

                }
                else if (catchedSpheres >= 100)
                {

                }

                intervalTime -= 0.005f;

                RemoveSphereFromList(i);
            }

            if (spheres[i].transform.position.y <= bottomLimit.position.y)
            {
                HandleSpherePassedBottomLimit(i);
            }
        }
    }

    void HandleSpherePassedBottomLimit (int index)
    {
        RemoveSphereFromList(index);

        lives--;

        if (lives <= 0)
        {
            gameOn = false;
            Invoke("RestartGame", 2f);
        }
    }

    void RemoveSphereFromList (int index)
    {
        GameObject temp = spheres[index];
        spheres.Remove(spheres[index]);
        speeds.Remove(speeds[index]);
        Destroy(temp);
    }

    void RestartGame ()
    {
        SceneManager.LoadScene(1);
    }

    bool checkCollision(Vector3 x, Vector3 y)
    {
        if (Mathf.Sqrt((y.x - x.x) * (y.x - x.x) + (y.y - x.y) * (y.y - x.y)) < (0.5f + 0.332f))
        {
            return true;
        }

        return false;
    }
}
