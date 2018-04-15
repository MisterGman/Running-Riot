using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.root.SendMessage("Damage");
            Debug.Log("---------------------------HIT");
        }
    }
}
