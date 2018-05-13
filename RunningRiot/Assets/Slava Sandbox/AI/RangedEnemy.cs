using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour {
    [HideInInspector]
    public enum State { Patrol, Chase, Hit, Stand };
    public State currState = State.Stand;
    public Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public bool facingRight;
    public float damage = 50f;
    private WeaponHit weapon;
    private bool lockHit = false;
    private bool seePlayer = false;
    public float aggroDistance = 5f;
    private bool teleportedOnce;
    public GameObject bullet;
    public Transform bulletInstantiatePosition;
    public float health = 50;
    private bool vulnatable = false;


    // Use this for initialization
    void Start()
    {
        this.tag = "Enemy";
        weapon = GetComponentInChildren<WeaponHit>();
        anim = GetComponent<Animator>();
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.transform.position + " player");
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
    void Update()
    {
        StayFixed();
        if (!vulnatable)
        {
            States();
            PlayerInRange();
        }
        

    }
    void States()
    {
        switch (currState)
        {
            case State.Stand:
                anim.SetTrigger("Stand");
                agent.isStopped = true;
                if (seePlayer)
                {
                    currState = State.Hit;
                    agent.isStopped = false;
                }

                break;
            case State.Patrol:

                break;
            case State.Chase:

                break;
            case State.Hit:
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                if (!lockHit)
                {
                    lockHit = !lockHit;
                    agent.isStopped = true;
                    StartCoroutine(HitPlayer());
                }
                if (Vector3.Distance(transform.position, player.position) < 2 && !teleportedOnce)
                {
                    Teleport();
                    teleportedOnce = true;
                }

                if (!seePlayer)
                {
                    currState = State.Stand;
                }
                break;
        }
    }
    void Teleport()
    {
        Vector3 randomDirection = Random.insideUnitSphere * aggroDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, aggroDistance, 1))
        {
            finalPosition = hit.position;
        }

        agent.enabled = false;
        transform.position =  finalPosition;
        agent.enabled = true;
    }
    bool CheckReachablePoint(Vector3 destination)
    {
        return NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, new NavMeshPath());
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
    void PlayerInRange()
    {
        if (!Physics2D.Linecast(transform.position, player.position, LayerMask.GetMask("Obsticle")) && Vector3.Distance(transform.position, player.position) < aggroDistance)
        {
            seePlayer = true;
            Debug.DrawLine(transform.position,player.position,Color.green);
        } else
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
            seePlayer = false;
        }
        
    }
    IEnumerator HitPlayer()
    {

        //anim.SetTrigger("Attack");
        Shoot();
        yield return new WaitForSeconds(2f);
        lockHit = !lockHit;
        agent.isStopped = false;
    }
    void Shoot()
    {
        Instantiate(bullet,bulletInstantiatePosition.position,Quaternion.Euler(0,0,-transform.eulerAngles.y));
    }
    void TurnOnHitbox()
    {
        weapon.GetComponent<Collider>().enabled = true;
        Debug.Log("------------EnabledCollider");
    }
    void TurnOffHitbox()
    {
        weapon.GetComponent<Collider>().enabled = false;
        Debug.Log("------------DisanabledCollider");
    }
    void Damage()
    {
        player.SendMessage("RecieveDamageFromEnemy", 25);
        weapon.GetComponent<Collider>().enabled = false;
        Debug.Log("--1241414134----------HIT");
    }
    void RecieveDamageFromPlayer(float damage)
    {
        StartCoroutine(Knockback());
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public IEnumerator Knockback()
    {
        vulnatable = true;
        if (player.position.x < transform.position.x)
        {
            agent.SetDestination(transform.position + new Vector3(2, 0, 0));
        }
        else
        {
            agent.SetDestination(transform.position - new Vector3(2, 0, 0));
        }
        yield return new WaitForSeconds(1.5f);
        vulnatable = false;
    }
}
