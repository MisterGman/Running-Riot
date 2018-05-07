using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageHit : MonoBehaviour {
    public bool knockback;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("HUI");
        if (other.tag == "Enemy")
        {
            other.transform.SendMessage("RecieveDamageFromPlayer", 100);
            if (knockback)
            {
                player.GetComponent<GraplingHook>().ReturnHook();
                StartCoroutine(player.Knockback(3f, 50, 5));
            }
        }
    }
}
