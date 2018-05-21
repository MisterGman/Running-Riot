using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour {
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit1");
        if (other.tag == "Hookable")
        {
            //Debug.Log(other.gameObject);
            player.GetComponent<GraplingHook>().hooked = true;
            player.GetComponent<GraplingHook>().hookedObj = other.gameObject;
        }
    }
}
