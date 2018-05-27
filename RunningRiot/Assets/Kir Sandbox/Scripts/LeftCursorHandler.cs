using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftCursorHandler : MonoBehaviour {
    private string d;
    private RectTransform rect;
    public Image Cursor;
    public Sprite right;
    public Sprite wrong;
    public Sprite defaultsp;
    public bool passGet;
    public RightCursorHandler rch;
    public int countwin;
    public int countlose;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();

        countwin = 0;
        countlose = 0;
        //passGet = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.W) && rect.transform.position.y < 225)
        {
            rect.position = new Vector2(rect.position.x, rect.position.y+50);
        }
        else if (Input.GetKeyUp(KeyCode.S) && rect.transform.position.y > -225)
        {
            rect.position = new Vector2(rect.position.x, rect.position.y - 50);
        }
        else if (Input.GetKeyUp(KeyCode.A) && rect.transform.position.x > -150)
        {
            rect.position = new Vector2(rect.position.x - 50, rect.position.y );
        }
        else if (Input.GetKeyUp(KeyCode.D) && rect.transform.position.x < 150)
        {
            rect.position = new Vector2(rect.position.x + 50, rect.position.y);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (passGet == true && rch.passGet == true)
            {
                Cursor.sprite = right;
                countwin++;
                Debug.Log("wins  " + countwin);
                
            }
            else
            {
                Cursor.sprite = wrong;
                countlose++;
                Debug.Log("loses  " + countlose);

            }
            Invoke("changeBack", 1f);
        }
    }
    void changeBack()
    {
        Cursor.sprite = defaultsp;
    }
    private void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.tag == "password1" /*&& collision.gameObject.tag == "password2"*/)
        {
            passGet = true;
        }
        else
        {
            passGet = false;
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "password1" /*&& collision.gameObject.tag == "password2"*/)
        {
            passGet = false;
        }
    }
}
