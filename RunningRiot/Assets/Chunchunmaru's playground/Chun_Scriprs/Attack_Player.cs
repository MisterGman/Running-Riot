using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" )
        {
            transform.root.SendMessage("Damage");
            Debug.Log("---------------------------HIT");
        }
    }
}
