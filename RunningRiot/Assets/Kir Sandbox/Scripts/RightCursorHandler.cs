using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightCursorHandler : MonoBehaviour
{
    private string d;
    private RectTransform rect;
    public Image Cursor;
    public Sprite right;
    public Sprite wrong;
    public Sprite defaultsp;
    public bool passGet;
    public LeftCursorHandler lfh;
    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //passGet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && rect.transform.position.y < 225)
        {
            rect.position = new Vector2(rect.position.x, rect.position.y + 50);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && rect.transform.position.y > -225)
        {
            rect.position = new Vector2(rect.position.x, rect.position.y - 50);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && rect.transform.position.x > -150)
        {
            rect.position = new Vector2(rect.position.x - 50, rect.position.y);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && rect.transform.position.x < 150)
        {
            rect.position = new Vector2(rect.position.x + 50, rect.position.y);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (passGet == true && lfh.passGet == true)
            {
                Cursor.sprite = right;
                //Debug.Log("Right " + passGet);
            }
            else
            {
                Cursor.sprite = wrong;
                //Debug.Log("Right " + passGet);
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
