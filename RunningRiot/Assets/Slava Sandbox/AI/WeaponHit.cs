using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    private Player player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.invincible == false)
        {
            transform.root.SendMessage("DamageFromWeapon");
            StartCoroutine(player.Knockback(0.1f, 15, 50));
        }
    }
}
