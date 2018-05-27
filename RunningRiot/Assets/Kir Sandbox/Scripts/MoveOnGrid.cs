using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    Vector3 pos;                                // For movement
    float speed = 400.0f;                         // Speed of movement

    void Start()
    {
        pos = transform.position;          // Take the initial position
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {        // Left
            pos += Vector3.left;
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right
            pos += Vector3.right * 101;
            Debug.Log("Right");
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            pos += Vector3.up * 101;
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            pos += Vector3.down * 101;
            Debug.Log("Down");
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }
}