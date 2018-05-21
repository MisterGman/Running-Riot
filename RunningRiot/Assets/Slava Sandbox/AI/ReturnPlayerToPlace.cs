using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToPlace : MonoBehaviour {
    [SerializeField]
    private Transform placeToReturn;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = placeToReturn.position;
        }
    }
}
