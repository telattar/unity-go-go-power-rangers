using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    public Transform playerPosition;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - playerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + playerPosition.position.z);
        transform.position = newPosition;
    }
}
