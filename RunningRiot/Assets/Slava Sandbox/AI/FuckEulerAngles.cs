﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckEulerAngles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0, 0, -(transform.parent.eulerAngles.z-90));
	}
}
