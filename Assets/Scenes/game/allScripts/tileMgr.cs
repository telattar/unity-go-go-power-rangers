using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMgr : MonoBehaviour
{
    public GameObject floor;
    public float zSpawn;
    public float floorLength = 50;
    public int numFloor = 5;
    public Transform playerTransform;
    private List<GameObject> floors = new List<GameObject>();

    void Start()
    {
        Time.timeScale = 1;

        for (int i = 0; i < numFloor; i++)
        {
            spawnFloor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 80 > zSpawn - (numFloor * floorLength))
        {
            spawnFloor();
            Destroy(floors[0]);
            floors.RemoveAt(0);
        }
    }

    public void spawnFloor()
    {
        GameObject newFloor = Instantiate(floor, transform.forward * zSpawn, transform.rotation);
        zSpawn += floorLength;
        floors.Add(newFloor);
    }
}
