using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class playerScript : MonoBehaviour
{
    public GameObject redOrb;
    private GameObject rightLane, middleLane, leftLane;
    public GameObject greenOrb;
    public GameObject blueOrb;
    public GameObject obstacle;
    public GameObject pausePanel;
    public static bool paused, resumeClicked;

    private GameObject obj;
    private List<GameObject> items = new List<GameObject>();
    public static List<GameObject> prefabs = new List<GameObject>();
    private Rigidbody control;
    private Vector3 direction;
    public float forwardSpeed;
    private int lane; //0 left 1 middle 2 right
    public float laneDistance; //distance between lanes
    void Start()
    {
        //plays the background music
        if (!muteMgr.mute)
            gameObject.GetComponent<AudioSource>().Play();

        Time.timeScale = 1;
        items.Add(redOrb);
        items.Add(greenOrb);
        items.Add(blueOrb);
        items.Add(null);
        items.Add(obstacle);

        control = GetComponent<Rigidbody>();
        lane = 1;
        laneDistance = 3.2f;
        //spawn obstacles
        InvokeRepeating("spawn", 1.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {

        if (collisionBehavior.gameoverBoolean)
            return;

        //to avoid any crashes, we remove the objects that are no longer on the screen

        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs.ElementAt(i) && prefabs.ElementAt(i).transform.position.z < control.transform.position.z - 5)
            {
                Destroy(prefabs[i]);
                prefabs.RemoveAt(i);
            }

        }

        direction.z = forwardSpeed;

        // change the lane upon the arrow button click
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && lane < 2)
        {
            lane++;
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && lane > 0)
        {
            lane--;
        }

        // calculate the player's new position
        Vector3 newPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lane == 0)
        {
            newPosition += Vector3.left * laneDistance;
        }
        else if (lane == 2)
        {
            newPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, 50 * Time.fixedDeltaTime);

        //pressing ESC opens/closes the pause screen
        if ((Input.GetKeyDown(KeyCode.Escape) && !collisionBehavior.gameoverBoolean) || resumeClicked)
        {
            managePausing();
            resumeClicked = false;
        }


    }

    void managePausing()
    {
        if (!paused)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            paused = true;
            if (!muteMgr.mute)
                pausePanel.GetComponent<AudioSource>().Play();


        }
        else if (paused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            paused = false;
            if (!muteMgr.mute)
                gameObject.GetComponent<AudioSource>().Play();

        }
    }

    private void FixedUpdate()
    {
        control.velocity = new Vector3(0, 0, direction.z) * Time.fixedDeltaTime;
    }

    void spawn()
    {

        //there should be 3 items in the lane, or 2 with no obstacles 
        leftLane = items.ElementAt(Random.Range(0, 5));
        middleLane = items.ElementAt(Random.Range(0, 5));

        //avoid having 3 obstacles
        if (leftLane == obstacle & middleLane == obstacle)
            rightLane = items.ElementAt(Random.Range(0, 4));

        else
            rightLane = items.ElementAt(Random.Range(0, 5));


        if (leftLane)
        {
            obj = Instantiate(leftLane, new Vector3(-3.2f, 0.4f, transform.position.z + 50), leftLane.transform.rotation);
            prefabs.Add(obj);
        }

        if (middleLane)
        {
            obj = Instantiate(middleLane, new Vector3(0.13f, 0.4f, transform.position.z + 50), middleLane.transform.rotation);
            prefabs.Add(obj);
        }
        if (rightLane)
        {
            obj = Instantiate(rightLane, new Vector3(3.2f, 0.4f, transform.position.z + 50), rightLane.transform.rotation);
            prefabs.Add(obj);
        }

    }

}
