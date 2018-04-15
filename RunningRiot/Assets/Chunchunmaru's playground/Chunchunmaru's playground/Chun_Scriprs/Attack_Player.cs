using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && Input.GetKeyDown(KeyCode.Keypad5))
        {
            transform.root.SendMessage("RecieveDamageFromEnemy");
            Debug.Log("---------------------------HIT");
        }
    }
}
