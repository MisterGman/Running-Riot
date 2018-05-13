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
	// Use this for initialization
	void Start () {

        images = new Sprite[5];

        images [0] = s0;
		images [1] = s1;
		images [2] = s2;
		images [3] = s3;
        images [4] = pass;

        changeImage();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeImage()
	{
		int num = UnityEngine.Random.Range(0, images.Length - 1);
		randomImage.sprite = images[num];
        Invoke("changeImage", 1f);
	}
    public void changeToPass()
    {
        randomImage.sprite = images[4];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
    }
}
