using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamageHitBox : MonoBehaviour {

    private Player player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HUI");
        if (other.tag == "Enemy" && player.invincible == false)
        {
            //other.transform.SendMessage("RecieveDamageFromPlayer");
            player.RecieveDamageFromEnemy(5);
            StartCoroutine(player.Knockback(3f, 10, 20));
        }
       
    }


}

