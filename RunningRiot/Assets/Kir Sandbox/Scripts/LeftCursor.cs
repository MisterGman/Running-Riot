using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCursor : MonoBehaviour {
    GameObject leftCursor;
    float x_pos;
    float y_pos;
    public float cellsize;
    // Use this for initialization
    void Start () {
        leftCursor = GameObject.FindGameObjectWithTag("LeftCursor");
        x_pos = 0.0f;
        y_pos = 0.0f;
        cellsize = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Down button was pressed");
            float curr_x = leftCursor.transform.position.x;
            float curr_y = leftCursor.transform.position.y;
            print(curr_x);
            print(curr_y);
            if (curr_y > 110)
            {
                leftCursor.transform.Translate(new Vector2(0, -130.82f));
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("Left button was pressed");
            float curr_x = leftCursor.transform.position.x;
            float curr_y = leftCursor.transform.position.y;
            print(curr_x);
            print(curr_y);
            if (curr_x > 212)
            {
                leftCursor.transform.Translate(new Vector2(-130.82f, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("Up button was pressed");
            float curr_x = leftCursor.transform.position.x;
            float curr_y = leftCursor.transform.position.y;
            print(curr_x);
            print(curr_y);
            if (curr_y < 605)
            {
                leftCursor.transform.Translate(new Vector2(0, 130.82f));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Right button was pressed");
            float curr_x = leftCursor.transform.position.x;
            float curr_y = leftCursor.transform.position.y;
            print(curr_x);
            print(curr_y);
            if (curr_x < 441)
            {
                {
                    leftCursor.transform.Translate(new Vector2(130.82f, 0));
                }
            }
        }
    }
}
