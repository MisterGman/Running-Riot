using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpears : MonoBehaviour {
    Animator anim;
    public GameObject rightSpear;
    public GameObject leftSpear;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("RightSpearIdle"))
        {
            rightSpear.SetActive(true);
            leftSpear.SetActive(false);
        } else
        {
            rightSpear.SetActive(false);
            leftSpear.SetActive(true);
        }
	}
}
