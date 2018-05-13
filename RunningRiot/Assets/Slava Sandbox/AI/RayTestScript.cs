using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTestScript : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        IfSeePlayer();
    }
    void IfSeePlayer()
    {
        if (!Physics2D.Linecast(transform.position, player.position,LayerMask.GetMask("Obsticle")))
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        } else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
        }
    }
}
