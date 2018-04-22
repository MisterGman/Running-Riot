using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour {
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit1");
        if (other.tag == "Hook")
        {
            Debug.Log("Hit2");
            player.GetComponent<GraplingHook>().hooked = true;
            player.GetComponent<GraplingHook>().hookedObj = this.gameObject;
        }
    }
}
