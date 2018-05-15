using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour {
    [SerializeField]
    private GameObject requeredEnemy;
    [SerializeField]
    private GameObject wallToRemove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (requeredEnemy == null)
        {
            Destroy(wallToRemove);
        }
	}
}
