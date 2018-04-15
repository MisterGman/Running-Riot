using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCursor : MonoBehaviour {
    GameObject rightCursor;
    float x_pos;
    float y_pos;
    public float cellsize;
    // Use this for initialization
    void Start () {
        rightCursor = GameObject.FindGameObjectWithTag("RightCursor");
        x_pos = 0.0f;
        y_pos = 0.0f;
        cellsize = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("A button was pressed");
            float curr_x = rightCursor.transform.position.x;
            float curr_y = rightCursor.transform.position.y;
            print(curr_x);
            print(curr_y);
            if (curr_y > 110)
            {
                rightCursor.transform.Translate(new Vector2(0, -130.82f));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("X button was pressed");
            float curr_x = rightCursor.transform.position.x;
            float curr_y = rightCursor.transform.position.y;
            print(curr_x);
            if (curr_x > 790)
            {
                rightCursor.transform.Translate(new Vector2(-130.82f, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("Y button was pressed");
            float curr_x = rightCursor.transform.position.x;
            float curr_y = rightCursor.transform.position.y;
            print(curr_y);
            if (curr_y < 605)
            {
                rightCursor.transform.Translate(new Vector2(0, 130.82f));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("B button was pressed");
            float curr_x = rightCursor.transform.position.x;
            float curr_y = rightCursor.transform.position.y;
            print(curr_x);
            if (curr_x < 1019)
            {
                rightCursor.transform.Translate(new Vector2(130.82f, 0));
            }
        }
    }
    
}
