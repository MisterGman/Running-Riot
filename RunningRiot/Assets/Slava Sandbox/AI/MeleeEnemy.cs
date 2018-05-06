﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour {
    private float myWidth;
    [HideInInspector]
    public enum State { Patrol, Chase, Hit, Stand, Flying };
    public State currState = State.Stand;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public bool facingRight;
    public float damage = 50f;
    private WeaponHit weapon;
    private bool lockHit = false;
    public float aggroDistance = 5f;
    public float aggroHeightDistance = 10f;
    private bool isGrounded = true;
    private GameObject particleS;
    int hp = 5;

    // Use this for initialization
    void Start () {
        particleS = GetComponentInChildren<ParticleSystem>().gameObject;
        particleS.SetActive(false);
        this.tag = "Enemy";
        weapon = GetComponentInChildren<WeaponHit>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if (transform.rotation.y < 0)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        StayFixed();
        States();
        IsGrounded();
        Debug.DrawLine(transform.position, transform.position + transform.forward);
        		
	}
    void IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up);
        if (Physics2D.Raycast(transform.position, -Vector3.up,0.1f))
        {
           // Debug.Log("Grounded");
            agent.speed = 3.5f;
            isGrounded = true;
            particleS.SetActive(false);
        } else
        {
           // Debug.Log("Not Grounded");
            agent.speed = 10f;
            isGrounded = false;
            particleS.SetActive(true);
        }
    }
    void States()
    {
        switch (currState)
        {
            case State.Stand:
                if (lockHit) currState = State.Hit;
                anim.SetTrigger("Stand");
                agent.isStopped = true;
                if (Vector3.Distance(transform.position, player.position) < aggroDistance && Mathf.Abs(transform.position.z - player.position.z) < aggroHeightDistance)
                {
                    currState = State.Chase;
                    agent.isStopped = false;
                }
                    
                break;
            case State.Patrol:
                anim.SetTrigger("Run");
                agent.stoppingDistance = 0f;
                if (CheckReachablePoint(transform.position + transform.forward))
                {
                    agent.SetDestination(transform.position + transform.forward);
                }
                else
                {
                    facingRight = !facingRight;
                }
                if (CheckReachablePoint(player.position) && Vector3.Distance(transform.position, player.position) < aggroDistance && Mathf.Abs(transform.position.y - player.position.y) < aggroHeightDistance)
                {
                    currState = State.Chase;
                }
                break;
            case State.Chase:
                anim.SetTrigger("Run");
                agent.stoppingDistance = 1f;
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                agent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < 2 && isGrounded)
                {
                    currState = State.Hit;
                }

                if (Vector3.Distance(transform.position, player.position) > aggroDistance || Mathf.Abs(transform.position.z - player.position.z) > aggroHeightDistance )
                {
                    currState = State.Stand;
                } 
                break;
            case State.Hit:
                if (Vector3.Distance(transform.position, player.position) > 2)
                {
                    currState = State.Chase;
                }
                if (!lockHit)
                {
                    lockHit = !lockHit;
                    agent.isStopped = true;
                    StartCoroutine(HitPlayer());
                }

                if (Vector3.Distance(transform.position, player.position) > 2)
                {
                    currState = State.Chase;
                }
                break;
        }
    }
    bool CheckReachablePoint(Vector3 destination)
    {
        return NavMesh.CalculatePath(transform.position,destination, NavMesh.AllAreas,new NavMeshPath());
    }
    void StayFixed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (!lockHit)
        {
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            }
        }
        
    }
    IEnumerator HitPlayer()
    {

        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
        lockHit = !lockHit;
        agent.isStopped = false;
    }

    void TurnOnHitbox()
    {
        weapon.GetComponent<Collider>().enabled = true;
        //Debug.Log("------------EnabledCollider");
    }
    void TurnOffHitbox()
    {
        weapon.GetComponent<Collider>().enabled = false;
        //Debug.Log("------------DisanabledCollider");
    }
    void DamageFromWeapon()
    {
        player.SendMessage("RecieveDamageFromEnemy", 25);
        weapon.GetComponent<Collider>().enabled = false;
    }
    void DamageFromBody()
    {
        player.SendMessage("RecieveDamageFromEnemy", 5);
    }

    void RecieveDamageFromPlayer()
    {
        Destroy(this.gameObject);  
    }
}
