using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomImage : MonoBehaviour {
	public Image randomImage;
    public int size;
	public Sprite s0;
	public Sprite s1;
	public Sprite s2;
	public Sprite s3;
    public Sprite pass;
    public Sprite[] images;
    private bool pword;
    private SetPassword setpassword;
	// Use this for initialization
	void Start () {

        images = new Sprite[5];
        images [0] = s0;
		images [1] = s1;
		images [2] = s2;
		images [3] = s3;
        images [4] = pass;
        pword = false;
        changeImage();
        setpassword = GameObject.FindGameObjectWithTag("PWRD").GetComponent<SetPassword>();
        Image[] pw1 = setpassword.pw1;
        Image[] pw2 = setpassword.pw2;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeImage()
	{
		int num = UnityEngine.Random.Range(0, images.Length - 1);
		randomImage.sprite = images[num];
        Invoke("changeImage", 1f);
        transform.gameObject.tag = "DaImage";
        if (pword == true)
        {
            pword = false;
        }
	}
    public void changeToPass()
    {
        randomImage.sprite = images[4];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        if (pword == false)
        {
            pword = true;
        }
    }
    public void changeToPass1()
    {
        randomImage.sprite = images[4];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        transform.gameObject.tag = "password1";
        if (pword == false)
        {
            pword = true;
        }
    }
    public void changeToPass2()
    {
        randomImage.sprite = images[4];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        transform.gameObject.tag = "password2";
        if (pword == false)
        {
            pword = true;
        }
    }
    /*private void OnTriggerStay(Collider collision)
    {
        
        
        if(collision.gameObject.tag == "LeftCursor" || collision.gameObject.tag == "RightCursor")
        {
            if (randomImage.sprite == pass)
            {
                Debug.Log("Oh, hi Mark");
            }
        }
    }*/
}
